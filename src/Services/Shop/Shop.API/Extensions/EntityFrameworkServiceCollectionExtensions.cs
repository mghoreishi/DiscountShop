using Shopping.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using IntegrationEventLogEF;
using System.Reflection;

namespace Shopping.API.Extensions
{
    public static class EntityFrameworkServiceCollectionExtensions
    {

        public static void AddShopContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ShopContext>(options =>
                          options.UseNpgsql(configuration.GetConnectionString("ShopConnectionString")),
                          ServiceLifetime.Scoped);

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("ShopConnectionString"),
                    npgsqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(ShopContext).GetTypeInfo().Assembly.GetName().Name);
                    });
            });
        }
    }
}
