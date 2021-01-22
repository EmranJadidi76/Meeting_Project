using DataLayer.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace WebframeworkLayer.Configurations
{


    public static class ClaimFactoryConfigureExtension
    {
        public static void ClaimFactoryConfiguration(this IServiceCollection services)
        {

            services.AddScoped<IUserClaimsPrincipalFactory<Users>, ApplicationClaimFactory>();
        }
    }
}
