using Microsoft.EntityFrameworkCore;
using StockProject.Data.Dtos;
using StockProject.Data.Entities;

namespace StockProject.Data.Repositories
{
    public class StockCategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public StockCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //c
        public async Task<int> CreateStockCategoryByEntity(StockCategory stockCategory)
        {
            _context.StockCategories.Add(stockCategory);
            return await _context.SaveChangesAsync();
        }


        public async Task<int> CreateStockCategoryByName(string name, string? description)
        {
            StockCategory stockCategory = new StockCategory()
            {
                Name = name,
                Description = description ?? string.Empty
            };
            _context.StockCategories.Add(stockCategory);
            return await _context.SaveChangesAsync();
        }

        //r
        public async Task<bool> NameExistAsync(string str)
        {
            return await _context.StockCategories.AnyAsync(p => p.Name.ToLower() == str.ToLower());
        }

        public async Task<List<StockCategory>> GetStockCategories()
        {
            return await _context.StockCategories.ToListAsync();
        }

        public async Task<StockCategory?> GetStockCategoryById(int id)
        {
            return await _context.StockCategories.FindAsync(id);
        }

        public async Task<StockCategory?> GetStockCategoryByName(string name)
        {
            return await _context.StockCategories
                .Where(s => s.Name.ToLower() == name.ToLower())
    .FirstOrDefaultAsync();
        }

        // u
        public async Task<int> UpdateStockCategory(StockCategory stockCategory)
        {
            _context.StockCategories.Update(stockCategory);
            return await _context.SaveChangesAsync();
        }

        //d
        public async Task<int> DeleteStockCategory(StockCategory stockCategory)
        {
            var stockCategory1 = await _context.StockCategories.FindAsync(stockCategory.CategoryId);
            if (stockCategory1 == null) return 0;
            _context.StockCategories.Remove(stockCategory1);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteStockCategoryById(int id)
        {
            var stockCategory = await _context.StockCategories.FindAsync(id);
            if (stockCategory == null) return 0;
            _context.StockCategories.Remove(stockCategory);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> DeleteStockCategoryByName(string name)
        {
            var stockCategory = await _context.StockCategories
                .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                .FirstOrDefaultAsync();
            if (stockCategory == null) return 0;
            _context.StockCategories.Remove(stockCategory);
            return await _context.SaveChangesAsync();
        }


    }
}
