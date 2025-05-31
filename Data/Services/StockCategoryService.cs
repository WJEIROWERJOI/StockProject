using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;
using StockProject.Data.Repositories;

namespace StockProject.Data.Services
{
    public class StockCategoryService
    {
        private readonly StockCategoryRepository _stockCategoryRespotiry;

        public StockCategoryService(StockCategoryRepository stockCategoryRespotiry)
        {
            _stockCategoryRespotiry = stockCategoryRespotiry;
        }



        //c

        //r
        public async Task<List<StockCategory>> FindAllStockCategories()
        {
            return await _stockCategoryRespotiry.GetStockCategories();
        }


        //u
        public async Task UpdateStockCategoryAsync(StockCategory stockCategory)
        {
            await _stockCategoryRespotiry.UpdateStockCategory(stockCategory);
        }




        //d
        public async Task DeleteStockCategoryAsync(StockCategory stockCategory)
        {
            await _stockCategoryRespotiry.DeleteStockCategory(stockCategory);
        }




    }
}
