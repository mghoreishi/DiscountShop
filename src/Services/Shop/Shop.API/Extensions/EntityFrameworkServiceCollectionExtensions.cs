using Shopping.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Shopping.API.Extensions
{
    public static class EntityFrameworkServiceCollectionExtensions
    {

        public static void AddShopContext(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ShopContext>(options =>
                          options.UseNpgsql(configuration.GetConnectionString("ShopConnectionString")),
                          ServiceLifetime.Scoped);

        }
    }
}
