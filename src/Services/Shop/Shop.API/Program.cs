using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shopping.Infrastructure.Data;

namespace Shop.API
{
    public class Program
    {
        public static string AppName = "Shop";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                //.MigrateDbContext<ShopContext>((context, services) =>
                //{
                //    //ILogger<ShopContextSeed> logger = services.GetService<ILogger<ShopContextSeed>>();

                //    //ShopContextSeed
                //    //    .SeedAsync(context, logger)
                //    //    .Wait();
                //})
                .Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((host, config) =>
                 {
                     config.AddEnvironmentVariables();
                 })
                 .UseStartup<Startup>();

        }
    }
}
