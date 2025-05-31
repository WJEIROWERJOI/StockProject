using System.Diagnostics;
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
        public async Task<StockEntity> CreateStockAsync(StockEntity stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
        //r
        public async Task<bool> NameExistAsync(string str)
        {
            return await _context.Stocks.AnyAsync(p => p.ProductName.ToLower() == str.ToLower());
        }
        public async Task<List<StockEntity>> GetAllStockAsync()
        {
            return await _context.Stocks
                .Include(s => s.Category)
                .ToListAsync();
        }

        public async Task<StockEntity?> GetStockByIdAsync(string id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<StockEntity?> GetStockByNameAsync(string name)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.ProductName == name);
        }
        //u

        public async Task UpdateStockAsync(StockEntity stock)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }


        //d
        public async Task DeleteStockAsync(string id)
        {
            var stock = await _context.Stocks
                .Include(s => s.Transactions)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }

    }
}
