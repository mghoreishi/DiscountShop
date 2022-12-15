using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shopping.API;
using Shopping.API.Options;
using Shopping.Infrastructure.Data;
using System.IO;
using System.Reflection;

namespace Shopping.IntegrationTests
{
    public class ShoppingScenariosBase
    {
        public TestServer CreateServer()
        {
            string path = Assembly.GetAssembly(typeof(ShoppingScenariosBase))
                .Location;

            IWebHostBuilder builder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                    .AddEnvironmentVariables();
                })
                .UseStartup<Startup>();

            TestServer testServer = new(builder);

            ShopContext context = testServer.Host.Services.GetService<ShopContext>();
            context.Database.EnsureDeleted();


            testServer.Host
                 .MigrateDbContext<ShopContext>((context, services) =>
                 {
                     IWebHostEnvironment env = services.GetService<IWebHostEnvironment>();
                     IOptions<ConnectionStringsOptions> settings = services.GetService<IOptions<ConnectionStringsOptions>>();
                     //ILogger<ShopContextSeed> logger = services.GetService<ILogger<ShopContextSeed>>();

                     //ShopContextSeed
                     //   .SeedAsync(context, logger)
                     //   .Wait();
                 })
                 ;

            return testServer;
        }

        public static class Get
        {


        }

        public static class Post
        {
            public static string CreateShop()
            {
                return "api/shop";
            }


        }

        public static class Put
        {

        }

        public static class Delete
        {

        }
    }
}