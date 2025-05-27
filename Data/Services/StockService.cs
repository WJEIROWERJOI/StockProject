using StockProject.Data.Dtos;
using StockProject.Data.Entities;
using StockProject.Data.Repositories;
using static StockProject.Data.Mapper;

namespace StockProject.Data.Services
{
    public class StockService
    {
        private readonly StockRepository _stockRepository;

        public StockService(StockRepository stockRepository)
        {
            _stockRepository = stockRepository;
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

        //d


    }
}
