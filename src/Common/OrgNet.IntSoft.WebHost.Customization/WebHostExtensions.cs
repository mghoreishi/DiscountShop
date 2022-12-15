using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace Microsoft.AspNetCore.Hosting
{
    public static class IWebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            IConfiguration configuration = webHost.Services.GetService<IConfiguration>();
            bool underK8s = IsInKubernetes(configuration);

            using (IServiceScope scope = webHost.Services.CreateScope())
            {
                Migrate(seeder, underK8s, scope);
            }

            return webHost;
        }
      

        private static void Migrate<TContext>(Action<TContext, IServiceProvider> seeder, bool underK8s, IServiceScope scope) where TContext : DbContext
        {
            IServiceProvider services = scope.ServiceProvider;
            ILogger<TContext> logger = services.GetRequiredService<ILogger<TContext>>();
            TContext context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                if (underK8s)
                {
                    InvokeSeeder(seeder, context, services);
                }
                else
                {                  
                    InvokeSeeder(seeder, context, services);

                }

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                context.Dispose();
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                if (underK8s)
                {
                    throw;          // Rethrow under k8s because we rely on k8s to re-run the pod
                }
            }
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }

        private static bool IsInKubernetes(IConfiguration cfg)
        {
            string orchestratorType = cfg.GetValue<string>("OrchestratorType");
            return orchestratorType?.ToUpper() == "K8S";
        }
    }
}
