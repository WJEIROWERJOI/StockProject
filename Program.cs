using Microsoft.AspNetCore.Identity;
using StockProject.Data;
using StockProject.Data.Entities;
using StockProject.Data.Services;
using StockProject.Extention;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureAppIdentity(builder.Configuration);
builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.AddAppServices();

var app = builder.Build();

app.ConfigureExceptionHandler();
app.ConfigureMiddleware();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        var logService = services.GetRequiredService<LogService>();

        await Seeding.SeedingBoard(context, roleManager, userManager);
    }
    catch (Exception ex)
    {
        // 필요 시 로그
        Console.WriteLine($"시딩 중 오류 발생: {ex.Message}");
    }
}



app.Run();





