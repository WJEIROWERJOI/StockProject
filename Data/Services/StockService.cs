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
            var category = await _stockCategoryRepository.GetStockCategoryByName(dto.Category)
                ?? throw new KeyNotFoundException($"Category '{dto.Category}' not found");

            try
            {
                StockEntity entity = StockDtoToEntity(dto, category);
                entity.CreatedAt = DateTime.UtcNow;
                entity.LastUpdatedAt = DateTime.UtcNow;

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
            var entity = await _stockRepository.GetStockByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Stock '{dto.ProductName}' not found");
            try
            {
                var transaction = await CreateStockTransactionAsync(entity, amount, "Amount changed");
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
                var transaction = await CreateStockTransactionAsync(entity, 0, "Stock changed");
                entity.Transactions.Add(transaction);
                entity.ProductName = dto.ProductName;
                entity.Description = dto.Description;
                entity.Category = await _stockCategoryRepository.GetStockCategoryByName(dto.Category)
                    ?? throw new KeyNotFoundException($"Category '{dto.Category}' not found");
                entity.Quantity = dto.Quantity;
                entity.LastUpdatedAt = DateTime.UtcNow;

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
        public async Task DeleteStockAsync(string id)
        {
            var stock = await _stockRepository.GetStockByIdAsync(id)
                ?? throw new KeyNotFoundException($"Stock with ID '{id}' not found");
            try
            {
                await _stockRepository.DeleteStockAsync(id);
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



        public async Task<StockTransaction> CreateStockTransactionAsync(StockEntity stock, int amount, string remark)
        {
            try
            {
                StockTransaction transaction = new StockTransaction
                {
                    StockId = stock.Id,
                    Quantity = amount,
                    PreviousQuantity = stock.Quantity,
                    AfterQuantity = stock.Quantity + amount,
                    Type = amount > 0 ? TransactionType.In : TransactionType.Out,
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
