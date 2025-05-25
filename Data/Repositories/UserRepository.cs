using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StockProject.Data.Entities;

namespace StockProject.Data.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserEntity> _userManager;
        public UserRepository(ApplicationDbContext context,UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        ////c

        //public async Task<IdentityResult> CreateUserByEntity(UserEntity userEntity)
        //{
        //    if (await NameExistAsync(stockCategory.Name)) { return 0; }
        //    _context.StockCategories.Add(stockCategory);
        //    return await _context.SaveChangesAsync();
        //}

        //public async Task<IdentityResult> CreateUserByEntity()


        //public async Task<int> CreateStockCategoryByName(string name, string? description)
        //{
        //    if (await NameExistAsync(name)) { return 0; }

        //    StockCategory stockCategoryEntity = new StockCategory()
        //    {
        //        Name = name,
        //        Description = description ?? string.Empty
        //    };

        //    _context.StockCategories.Add(stockCategoryEntity);
        //    return await _context.SaveChangesAsync();
        //}




        ////r
        //public async Task<bool> NameExistAsync(string str)
        //{
        //    return await _context.StockCategories.AnyAsync(p => p.Name.Equals(str, StringComparison.OrdinalIgnoreCase));


        //}
        ////MapperClass
        ////public StockCategory EntityToDto(StockCategorydto dto)
        ////{
        ////    return new StockCategory()
        ////    {
        ////        Name = dto.Name,
        ////        Description = dto.Description ?? string.Empty
        ////    };
        ////}



        //public async Task<List<StockCategory>> GetStockCategories()
        //{
        //    return await _context.StockCategories.ToListAsync();
        //}


        //public async Task<StockCategory?> GetStockCategoryById(int id)
        //{
        //    return await _context.StockCategories.FindAsync(id);
        //}

        //public async Task<StockCategory?> GetStockCategoryByName(string name)
        //{
        //    return await _context.StockCategories
        //        .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
        //        .FirstOrDefaultAsync();
        //}


        //// u
        //public async Task<int> UpdateStockCategory(StockCategory stockCategory)
        //{
        //    if (await NameExistAsync(stockCategory.Name)) { return 0; }
        //    _context.StockCategories.Update(stockCategory);
        //    return await _context.SaveChangesAsync();
        //}


        ////d
        //public async Task<int> DeleteStockCategory(StockCategory stockCategory)
        //{
        //    _context.StockCategories.Remove(stockCategory);
        //    return await _context.SaveChangesAsync();
        //}




    }
}
