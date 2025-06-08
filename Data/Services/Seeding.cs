using Microsoft.AspNetCore.Identity;
using StockProject.Data.Entities;

namespace StockProject.Data.Services;

public static class Seeding
{
    public static async Task SeedingBoard(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<UserEntity> userManager)
    {
        var logService = new LogService();

        if (!context.Boards.Any())
        {
            List<Board> brds = new List<Board> {
            new Board { Title = "StockList", Url = "/stock/list", Img = "bi bi-plus-square-fill-nav-menu",Primary=true },
            new Board { Title = "StockCategory", Url = "/stock/category", Img = "bi bi-lock-nav-menu",Primary=true },
            new Board {Title = "UserLogin",
                Url="/Account/Login",
                Img="bi bi-plus-square-fill-nav-menu",
                Primary=true,
                Boards = new List<Board> {
                                    new Board {
                                                    Title = "Register",
                                                    Url = "/User/Register"
                                               }

                                          }
        },
            new Board { Title = "Gamble", Url = "/Gamble", Img = "bi bi-plus-square-fill-nav-menu",Primary=true }
            };
            context.Boards.AddRange(brds);
        }


        if (!context.StockCategories.Any())
        {
            List<StockCategory> cas = new()
                {
                    new StockCategory
                    {
                        Name = "Electronics",
                        Description ="전자 제품"
                    },
                    new StockCategory
                    {
                        Name = "Furniture",
                        Description ="가구"
                    },
                    new StockCategory
                    {
                        Name = "Food",
                        Description ="음식"
                    }
                };
            context.StockCategories.AddRange(cas);
            var result = await context.SaveChangesAsync();
            if (result != 0)
            {
                foreach (var c in cas)
                {
                    await logService.LogAsync("CreateStockCategory", c.Name);
                }
            }

        }

        if (!context.Stocks.Any())
        {

            List<StockEntity> cas = new List<StockEntity>
                {
                    new() {
                        Category=context.StockCategories.Single(p=>p.Name.Equals("Electronics")),
                        ProductName="Iphone",
                        Description="아이폰",
                        Quantity =1,
                        CreatedAt = DateTime.UtcNow,
                        LastUpdatedAt = DateTime.UtcNow
                    },
                    new() {
                        Category=context.StockCategories.Single(p=>p.Name.Equals("Furniture")),
                        ProductName="Sidiz",
                        Description="시디즈책상",
                        Quantity =3,
                        CreatedAt = DateTime.UtcNow,
                        LastUpdatedAt = DateTime.UtcNow
                    },
                    new() {
                        Category=context.StockCategories.Single(p=>p.Name.Equals("Food")),
                        ProductName="McDonald",
                        Description="햄벅어",
                        Quantity =5,
                        CreatedAt = DateTime.UtcNow,
                        LastUpdatedAt = DateTime.UtcNow
                    },

                };

            context.Stocks.AddRange(cas);


            var result = await context.SaveChangesAsync();
            if (result != 0)
            {
                foreach (var c in cas)
                {
                    await logService.LogAsync("CreateStock", c.ProductName);
                }
            }
        }


        if (!await roleManager.RoleExistsAsync("SuperAdmin"))
        {
            await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            await logService.LogAsync("CreateRole", "SuperAdmin");
        }
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
            await logService.LogAsync("CreateRole", "Admin");
        }
        if (!await roleManager.RoleExistsAsync("Staff"))
        {
            await roleManager.CreateAsync(new IdentityRole("Staff"));
            await logService.LogAsync("CreateRole", "Staff");
        }

        if (!context.Users.Any())
        {

            List<UserEntity> usrs = new()
                {
                    new UserEntity
                    {
                         UserName = "superadmin",
                    Email = "suadmin@suadmin.com"
                    },
                    new UserEntity
                    {
                        UserName = "admin",
                    Email = "admin@admin.com"
                    },
                    new UserEntity
                    {
                        UserName = "test1",
                    Email = "test1@test1.com"
                    }
                };

            foreach (var usr in usrs)
            {
                var result = await userManager.CreateAsync(usr, usr.UserName ?? string.Empty);
                if (result == IdentityResult.Success)
                {
                    await logService.LogAsync("CreateUser", usr.UserName ?? string.Empty);

                }
                switch (usr.UserName)
                {
                    case "superadmin":
                        await userManager.AddToRolesAsync(usr, new List<string> { "SuperAdmin", "Admin" });
                        await logService.LogAsync("AddRole", $"{usr.UserName} to SuperAdmin and Admin");
                        break;
                    case "admin":
                        await userManager.AddToRoleAsync(usr, "Admin");
                        await logService.LogAsync("AddRole", $"{usr.UserName} to Admin");
                        break;
                    case "test1":
                        await userManager.AddToRoleAsync(usr, "Staff");
                        await logService.LogAsync("AddRole", $"{usr.UserName} to Staff");
                        break;
                }

            }


        }
    }

}