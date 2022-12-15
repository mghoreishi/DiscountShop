using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Discounting.API;
using Discounting.API.Options;
using Discounting.Infrastructure.Data;
using System.IO;
using System.Reflection;

namespace Discounting.IntegrationTests
{
    public class DiscountingScenariosBase
    {
        public TestServer CreateServer()
        {
            string path = Assembly.GetAssembly(typeof(DiscountingScenariosBase))
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

            DiscountContext context = testServer.Host.Services.GetService<DiscountContext>();
            context.Database.EnsureDeleted();

            context.Database.Migrate();

            return testServer;
        }

        public static class Get
        {


        }

        public static class Post
        {
            public static string CreateDiscount()
            {
                return "api/Discount";
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