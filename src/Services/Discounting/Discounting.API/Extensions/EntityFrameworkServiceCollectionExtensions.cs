using Discounting.Infrastructure.Data;
using IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Discounting.API.Extensions
{
    public static class EntityFrameworkServiceCollectionExtensions
    {

        public static void AddDiscountContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DiscountContext>(options =>
                          options.UseNpgsql(configuration.GetConnectionString("DiscountConnectionString")),
                          ServiceLifetime.Scoped);

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DiscountConnectionString"),
                    npgsqlOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(DiscountContext).GetTypeInfo().Assembly.GetName().Name);
                    });
            });

        }
    }
}
