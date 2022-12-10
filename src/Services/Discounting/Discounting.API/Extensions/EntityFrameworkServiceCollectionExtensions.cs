using Discounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Discounting.API.Extensions
{
    public static class EntityFrameworkServiceCollectionExtensions
    {

        public static void AddDiscountContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DiscountContext>(options =>
                          options.UseNpgsql(configuration.GetConnectionString("DiscountConnectionString")),
                          ServiceLifetime.Scoped);

        }
    }
}
