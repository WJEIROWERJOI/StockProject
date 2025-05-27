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
            var entity = await _stockRepository.GetStockByNameAsync(dto.ProductName)
                ?? throw new KeyNotFoundException($"Stock '{dto.ProductName}' not found");


            try
            {
                await CreateStockTransactionAsync(entity, amount, "Amount changed");

                entity.Quantity += amount;
                entity.LastUpdatedAt = DateTime.UtcNow;

                await _stockRepository.UpdateStockAsync(entity);

            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
     "StockTransactionError",
     $"Exception Type: {ex.GetType().Name}, Message: {ex.Message}, StackTrace: {ex.StackTrace}"
 );
                throw;
            }
        }



        //d

        public async Task CreateStockTransactionAsync(StockEntity stock, int amount, string remark)
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
                return;
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
    "StockTransactionError",
    $"Exception Type: {ex.GetType().Name}, Message: {ex.Message}, StackTrace: {ex.StackTrace}"
);
                throw;
            }
        }



    }
}
