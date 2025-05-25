using Microsoft.AspNetCore.Components.Authorization;
using StockProject.Components.Account;

namespace StockProject.Extention;
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
          
            //services.AddScoped<ApplicationUserService>();
            //services.AddScoped<BoardService>();
            //services.AddScoped<CommentService>();
            //services.AddScoped<PostService>();


            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
            services.AddRazorComponents()
                .AddInteractiveServerComponents();
            services.AddCascadingAuthenticationState();

            return services;
        }
    }

