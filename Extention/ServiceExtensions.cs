using Microsoft.AspNetCore.Components.Authorization;
using StockProject.Components.Pages.Account;
using StockProject.Data.Repositories;
using StockProject.Data.Services;

namespace StockProject.Extention;
public static class ServiceExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {

        //services.AddScoped<UserEntityService>();
        //services.AddScoped<BoardService>();
        //services.AddScoped<CommentService>();
        //services.AddScoped<PostService>();

        services.AddScoped<StockRepository>();
        services.AddScoped<StockService>();
        services.AddScoped<LogService>();
        services.AddScoped<UserService>();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
        services.AddRazorComponents()
            .AddInteractiveServerComponents();
        services.AddCascadingAuthenticationState();

        return services;
    }
}

