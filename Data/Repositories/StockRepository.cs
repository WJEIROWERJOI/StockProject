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



        //r
        public async Task<List<StockEntity>> GetAllStockAsync()
        {
            return await _context.Stocks
                .Include(s => s.Category)
                .ToListAsync();
        }

        public async Task<StockEntity> GetStockByIdAsync(string id)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id)
                   ?? throw new KeyNotFoundException("Stock not found");
        }

        public async Task<StockEntity> GetStockByNameAsync(string name)
        {
            return await _context.Stocks.FirstOrDefaultAsync(s => s.ProductName == name)
                   ?? throw new KeyNotFoundException("Stock not found");
        }
        //u

        public async Task<bool> UpdateStockAsync(StockEntity stock)
        {
            try
            {
                _context.Stocks.Update(stock);  // 엔티티 변경 추적
                await _context.SaveChangesAsync();  // 변경 사항 DB에 반영
            }
            catch (Exception ex)
            {
                // 예외 처리 (로그 기록 등)
                Console.WriteLine($"업데이트 중 오류 발생: {ex.Message}");
                return false;
            }
            return true;
        }

        //d


    }
}
