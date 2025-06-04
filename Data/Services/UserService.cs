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
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly LogService _logService;
        public UserService(ApplicationDbContext context, UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager,LogService logService,SignInManager<UserEntity> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logService = logService;
            _signInManager = signInManager;
        }
        
        //c
        public async Task CreateUser(UserRegisterDto dto)
        {
            UserEntity user = UserRegisterDtoToEntity(dto);
         var result = await _userManager.CreateAsync(user, dto.Password);
            if(result == IdentityResult.Success)
            {
                await _logService.LogAsync("CreateUser", user.UserName ?? "Unknown");
            }
         var result1 = await _userManager.AddToRoleAsync(user, dto.Role.ToString());
            if (result1 == IdentityResult.Success)
            {
                await _logService.LogAsync("AddRole", $"{user.UserName} to {dto.Role.ToString()}");
            }






        }
        //r
        public async Task<UserEntity?> GetUserByNameAsync(string str)
        {
            return await _userManager.FindByNameAsync(str);
        }


        //u
        //d


        //Login
        public async Task<IdentityResult> UserSignIn(string username,string password,bool remember)
        {
            var result = await _signInManager.PasswordSignInAsync(username,password,remember, false);
            //Console.WriteLine(result);
            if (result.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "fail login" });
            }
        }






    }
}
