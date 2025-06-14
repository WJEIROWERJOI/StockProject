﻿using Microsoft.EntityFrameworkCore;
using StockProject.Data.Dtos;
using StockProject.Data.Entities;
using StockProject.Data.Repositories;
using static StockProject.Data.Mapper;

namespace StockProject.Data.Services
{
    public class StockService
    {
        private readonly StockRepository _stockRepository;
        private readonly StockCategoryRepository _stockCategoryRepository;
        private readonly StockTransactionRepository _stockTransactionRepository;
        private readonly LogService _logService;
        public StockService(
            StockRepository stockRepository,
        StockCategoryRepository stockCategoryRepository,
        StockTransactionRepository stockTransactionRepository,
        LogService logService)
        {
            _logService = logService;
            _stockCategoryRepository = stockCategoryRepository;
            _stockRepository = stockRepository;
            _stockTransactionRepository = stockTransactionRepository;
        }

        //c
        public async Task CreateStockAsync(StockDto dto)
        {

            if (await _stockRepository.NameExistAsync(dto.ProductName))
            {
                throw new Exception($"Stock '{dto.ProductName}' already exists.");
            }
            try
            {
                var category = await _stockCategoryRepository.GetStockCategoryByName(dto.Category)
                                                ?? throw new Exception($"Category '{dto.Category}' not found");

                var entity = StockDtoToEntity(dto, category);
                entity.CreatedAt = DateTime.UtcNow;
                entity.LastUpdatedAt = DateTime.UtcNow;
                var transaction = await CreateStockTransactionAsync(entity, dto.Amount, "Stock Created", dto.UserId ?? string.Empty);
                entity.Transactions.Add(transaction);

                category.Stocks.Add(entity);

                await _stockRepository.CreateStockAsync(entity);
                
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    "StockCreationError",
                    $"Exception Type: {ex.GetType().Name}, Message: {ex.Message}, StackTrace: {ex.StackTrace}"
                );
                throw;
            }
        }



        //r
        public async Task<List<StockDto>> FindAllStockAsync()
        {
            var entities = await _stockRepository.GetAllStockAsync();
            var dtos = entities.Select(e => StockEntityToDto(e)).ToList();
            return dtos;
        }

        //u
        public async Task ChangeAmountAsync(StockDto dto, int amount)
        {
            if (amount == 0)
            {
                return;
            }
            var entity = await _stockRepository.GetStockByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Stock '{dto.ProductName}' not found");
            try
            {
                var transaction = await CreateStockTransactionAsync(entity, amount, "Amount changed", dto.UserId ?? string.Empty);
                entity.Transactions.Add(transaction);
                entity.Quantity += amount;
                entity.LastUpdatedAt = DateTime.UtcNow;
                await _stockRepository.UpdateStockAsync(entity);
            }
            catch (Exception ex)
            {
                // await _logService.LogAsync(ex.GetType().Name, $"{ex.StackTrace} + {ex.Message}");
                await _logService.LogAsync(
                    "StockChangeAmountError",
                    $"Exception Type: {ex.GetType().Name}, Message: {ex.Message}, StackTrace: {ex.StackTrace}"
                );
                throw;
            }
        }

        public async Task ChangeStock(StockDto dto)
        {
            var entity = await _stockRepository.GetStockByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Stock '{dto.ProductName}' not found");
            try
            {
                entity.ProductName = dto.ProductName;
                entity.Description = dto.Description ?? string.Empty;

                var oldCategory = entity.Category;
                entity.Category = await _stockCategoryRepository.GetStockCategoryByName(dto.Category)
                    ?? throw new KeyNotFoundException($"Category '{dto.Category}' not found");

                if (oldCategory != null)
                {
                    oldCategory.Stocks.Remove(entity);
                    //await _stockCategoryRepository.UpdateStockCategory(oldCategory);
                }

                entity.Category.Stocks.Add(entity);

                //await _stockCategoryRepository.UpdateStockCategory(entity.Category);

                entity.Quantity = dto.Quantity;
                entity.LastUpdatedAt = DateTime.UtcNow;

                var transaction = await CreateStockTransactionAsync(entity, 0, "Stock changed", dto.UserId ?? string.Empty);
                entity.Transactions.Add(transaction);

                await _stockRepository.UpdateStockAsync(entity);
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    "StockChangeError",
                    $"Exception Type: {ex.GetType().Name}, Message: {ex.Message}, StackTrace: {ex.StackTrace}"
                );
                throw;
            }

        }


        //d
        public async Task DeleteStockAsync(StockDto dto)
        {
            var stock = await _stockRepository.GetStockByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Stock with ID '{dto.ProductName}' not found");
            try
            {
                var category = stock.Category;
                if (category != null)
                {
                    category.Stocks.Remove(stock);
                    await _stockCategoryRepository.UpdateStockCategory(category);
                }
                await CreateStockTransactionAsync(stock, 0, "Stock deleted", dto.UserId ?? string.Empty);
                await _stockRepository.DeleteStockAsync(stock.Id);
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    "StockDeletionError",
                    $"Exception Type: {ex.GetType().Name}, Message: {ex.Message}, StackTrace: {ex.StackTrace}"
                );
                throw;
            }
        }



        public async Task<StockTransaction> CreateStockTransactionAsync(StockEntity stock, int amount, string remark, string userId)
        {
            try
            {
                StockTransaction transaction = new StockTransaction
                {
                    UserId = userId,
                    StockId = stock.Id,
                    Quantity = amount,
                    PreviousQuantity = stock.Quantity,
                    AfterQuantity = stock.Quantity + amount,
                    Type = amount > 0 ? TransactionType.In : (amount < 0 ? TransactionType.Out : TransactionType.Change),
                    Remark = remark,
                    TransactionDate = DateTime.UtcNow
                };
                await _stockTransactionRepository.CreateStockTransactionAsync(transaction);

                return transaction;
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    "TransactionCreationError",
                    $"Exception Type: {ex.GetType().Name}, Message: {ex.Message}, StackTrace: {ex.StackTrace}"
                );
                throw;
            }
        }

    }
}
