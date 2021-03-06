﻿using Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebframeworkLayer.Configurations;

namespace Meeting_Project
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.DatabaseConfiguration(Configuration);

            services.AddCollectionServices(Configuration);

            services.AddCustomIdentity(_siteSetting.IdentitySettings);

            services.ClaimFactoryConfiguration();

            services.AddAuthentication()
             .Services.ConfigureApplicationCookie(options =>
             {
                 options.SlidingExpiration = true;
                 options.ExpireTimeSpan = TimeSpan.FromDays(90);
             });

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRedirectConfigure();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();


        }
    }
}
