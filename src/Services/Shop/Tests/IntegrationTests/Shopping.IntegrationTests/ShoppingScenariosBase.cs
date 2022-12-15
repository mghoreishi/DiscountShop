using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
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

            context.Database.Migrate();

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