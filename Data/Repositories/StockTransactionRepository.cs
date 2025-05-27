

    using Microsoft.EntityFrameworkCore;
    using StockProject.Data.Entities;

namespace StockProject.Data.Repositories;
    public class StockTransactionRepository
    {
        private readonly ApplicationDbContext _context;

        public StockTransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //c
        public async Task<int> CreateStockTransactionAsync(StockTransaction transaction)
        {
            _context.StockTransactions.Add(transaction);
            return await _context.SaveChangesAsync();
        }

        //r
        public async Task<List<StockTransaction>> GetTransactionsByStockIdAsync(string stockId)
        {
            return await _context.StockTransactions
                .Where(t => t.StockId == stockId)
                .ToListAsync();
        }
    }
