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
        private readonly LogService _logService;
        public UserService(ApplicationDbContext context, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager,LogService logService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logService = logService;
        }
        
        //c
        public async Task CreateUser(UserRegisterDto dto)
        {
            UserEntity user = UserRegisterDtoToEntity(dto);
         var result = await _userManager.CreateAsync(user, dto.Password);
            if(result == IdentityResult.Success)
            {
                await _logService.LogAsync("CreateUser", user.UserName);
            }
         var result1 = await _userManager.AddToRoleAsync(user, dto.Role.ToString());
            if (result1 == IdentityResult.Success)
            {
                await _logService.LogAsync("AddRole", $"{user.UserName} to {dto.Role.ToString()}");
            }






        }









    }
}
