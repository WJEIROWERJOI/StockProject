using Microsoft.AspNetCore.Identity;
using StockProject.Data.Dtos;
using StockProject.Data.Entities;
using static StockProject.Data.Mapper;
namespace StockProject.Data.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ApplicationDbContext context, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        //c
        public async Task CreateUser(UserRegisterDto dto)
        {
            UserEntity user = UserRegisterDtoToEntity(dto);
         var result = await _userManager.CreateAsync(user, dto.Password);
         var result1 = await _userManager.AddToRoleAsync(user, dto.Role.ToString());





            public class IdentityResult
        {
            public bool Succeeded { get; }
            public IEnumerable<IdentityError> Errors { get; }

            public static IdentityResult Success { get; }
            public static IdentityResult Failed(params IdentityError[] errors);
        }
    }









    }
}
