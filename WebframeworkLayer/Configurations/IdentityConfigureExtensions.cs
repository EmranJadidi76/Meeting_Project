using Core;
using DataLayer.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;
using System;

namespace WebframeworkLayer.Configurations
{
    public static class IdentityConfigureExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings settings)
        {
            services.AddIdentity<Users, Roles>(identityOptions =>
            {
                //Password Settings
                identityOptions.Password.RequireDigit = settings.PasswordRequireDigit;
                identityOptions.Password.RequiredLength = settings.PasswordRequiredLength;
                identityOptions.Password.RequireNonAlphanumeric = settings.PasswordRequireNonAlphanumic; 
                identityOptions.Password.RequireUppercase = settings.PasswordRequireUppercase;
                identityOptions.Password.RequireLowercase = settings.PasswordRequireLowercase;

                //UserName Settings
                identityOptions.User.RequireUniqueEmail = settings.RequireUniqueEmail;
             
                //Lockout Settings
                identityOptions.Lockout.MaxFailedAccessAttempts = 10;
                identityOptions.Lockout.DefaultLockoutTimeSpan = DateTime.Now.Subtract(DateTime.UtcNow).Add(TimeSpan.FromSeconds(5));
                identityOptions.Lockout.AllowedForNewUsers = false;

                //identityOptions.Cookies.ApplicationCookie.ExpireTimeSpan = TimeSpan.FromHours(10);
            })
            .AddEntityFrameworkStores<DatabaseContext>()
            .AddDefaultTokenProviders();
        }
    }
}
