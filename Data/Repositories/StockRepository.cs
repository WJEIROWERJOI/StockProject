using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;

namespace StockProject.Data.Repositories
{
    public class StockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }




        //c



        //r
        public async Task<List<StockEntity>> GetAllStockAsync()
        {
            return await _context.Stocks.Include(s=>s.Category)
                .ToListAsync();
        }


        //u

        //d


    }
}
