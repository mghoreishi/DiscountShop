using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Discounting.Infrastructure.Data;

namespace Discounting.API
{
    public class Program
    {
        public static string AppName = "Discounting";
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                //.MigrateDbContext<DiscountContext>((context, services) =>
                //{
                //    //ILogger<DiscountContextSeed> logger = services.GetService<ILogger<DiscountContextSeed>>();

                //    //DiscountContextSeed
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
