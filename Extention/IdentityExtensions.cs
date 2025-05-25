using Microsoft.AspNetCore.Identity;
using StockProject.Components.Pages.Account;
using StockProject.Data;
using StockProject.Data.Entities;

namespace StockProject.Extention;

    public static class IdentityExtensions
    {
        public static IServiceCollection ConfigureAppIdentity(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<UserEntity>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddSignInManager()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            }).AddIdentityCookies();
            //services.AddSingleton<IEmailSender<UserEntity>, IdentityNoOpEmailSender>();
            return services;
        }


    }

