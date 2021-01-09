using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;
using ServiceLayer.Mappers;
using System.Data;
using System.Data.SqlClient;

namespace WebframeworkLayer.Configurations
{

    public static class DatabaseConfigurationExtensions
    {
        public static void DatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DatabaseContext>(options
                => options.UseSqlServer(configuration.GetConnectionString("MyConnection")));

            services.AddSingleton<IDbConnection>(
       _ => new SqlConnection(configuration.GetConnectionString("MyConnection")));


            services.Scan(scan =>
                scan.FromAssemblyOf<DatabaseContext>()
                    .AddClasses(classes => classes.InNamespaceOf<DatabaseContext>())
                    .AsSelf()
                    .WithScopedLifetime());

            #region Mapper
            services.AddAutoMapper(typeof(UserMappingProfile));
            #endregion
        }

    }
}
