﻿using Microsoft.AspNetCore.Builder;

namespace WebframeworkLayer.Configurations
{
    public static class RedirectConfigureExtentions
    {
        public static void UseRedirectConfigure(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 403)
                {
                    context.Request.Path = "/Home/Error403";
                    await next();
                }
            });

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/Home/Error404";
                    await next();
                }
            });
        }
    }
}
