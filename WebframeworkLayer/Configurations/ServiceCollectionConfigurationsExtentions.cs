using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;

namespace WebframeworkLayer.Configurations
{
    public static class ServiceCollectionConfigurationsExtentions
    {
        public static void AddCollectionServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Scan(scan =>
                scan.FromAssemblyOf<DatabaseContext>()
                    .AddClasses(classes => classes.InNamespaceOf<DatabaseContext>())
                    .AsSelf()
                    .WithScopedLifetime());

        }
    }
}
