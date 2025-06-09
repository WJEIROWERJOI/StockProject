using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.SessionStorage;
using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Authorization;
using StockProject.Components.Pages.Account;
using StockProject.Data.Repositories;
using StockProject.Data.Services;

namespace StockProject.Extention;

public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {

        services.AddBlazoredSessionStorage();
        services.AddBlazoredLocalStorage();
        
        services.AddScoped<PointRepository>();
        services.AddScoped<PointService>();

        services.AddBlazoredModal();
        services.AddBlazoredToast();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddScoped<BoardRepository>();
        services.AddScoped<BoardService>();

        services.AddScoped<StockCategoryService>();
        services.AddScoped<StockTransactionRepository>();
        services.AddScoped<StockCategoryRepository>();
        services.AddScoped<StockRepository>();
        services.AddScoped<StockService>();
        services.AddScoped<LogService>();
        services.AddScoped<UserService>();
        services.AddScoped<UserRepository>();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        services.AddRazorComponents()
            .AddInteractiveServerComponents();
        services.AddCascadingAuthenticationState();

        //var repositoryTypes = typeof(StockRepository).Assembly.GetTypes()
        //.Where(t => t.Name.EndsWith("Repository"));
        //foreach (var type in repositoryTypes)
        //{
        //    services.AddScoped(type);
        //}

        //var serviceTypes = typeof(StockService).Assembly.GetTypes()
        //.Where(t => t.Name.EndsWith("Service"));
        //foreach (var type in serviceTypes)
        //{
        //    services.AddScoped(type);
        //}



        return services;
    }
}

