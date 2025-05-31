using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;
using StockProject.Data.Repositories;

namespace StockProject.Data.Services;
public class StockCategoryService
{
    private readonly StockCategoryRepository _stockCategoryRepository;
    private readonly LogService _logService;

    public StockCategoryService(StockCategoryRepository stockCategoryRespotiry, LogService logService)
    {
        _logService = logService;
        _stockCategoryRepository = stockCategoryRespotiry;
    }



    //c
    public async Task CreateStockCategory(StockCategory stockCategory)
    {
        if (await _stockCategoryRepository.NameExistAsync(stockCategory.Name))
        {
            throw new Exception($"Stock '{stockCategory.Name}' already exists.");
        }

        try
        {
            await _stockCategoryRepository.CreateStockCategoryByEntity(stockCategory);
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
    public async Task<List<StockCategory>> FindAllStockCategories()
    {
        return await _stockCategoryRepository.GetStockCategories();
    }
    public async Task<bool> CheckStockCategoryStocks(StockCategory stockCategory)
    {
        var entity = await _stockCategoryRepository.GetStockCategoryById(stockCategory.CategoryId)
                ?? throw new Exception($"Stock '{stockCategory.Name}' not found");
        return entity.Stocks.Any();
    }

    //u
    public async Task UpdateStockCategoryAsync(StockCategory stockCategory)
    {
        var entity = await _stockCategoryRepository.GetStockCategoryById(stockCategory.CategoryId)
            ?? throw new Exception($"Stock '{stockCategory.Name}' not found");
        try
        {
            await _stockCategoryRepository.UpdateStockCategory(stockCategory);
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
    public async Task DeleteStockCategoryAsync(StockCategory stockCategory)
    {
        var entity = await _stockCategoryRepository.GetStockCategoryById(stockCategory.CategoryId)
            ?? throw new Exception($"Stock with ID '{stockCategory.Name}' not found");
        try
        {
            await _stockCategoryRepository.DeleteStockCategory(entity);
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
}

