using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Discounting.API.Extensions
{
    public static class AutofacConfigurationExtensions
    {
        /// <summary>
        /// Register Services to Autofac ContainerBuilder
        /// </summary>
        /// <param name="containerBuilder"></param>
        public static void AddServices(this ContainerBuilder containerBuilder)
        {
            //RegisterType > As > Liftetime
          
        }

        public static IServiceProvider BuildAutofacServiceProvider(this IServiceCollection services)
        {
            ContainerBuilder containerBuilder = new();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            containerBuilder.Populate(services);

            containerBuilder.AddServices();

            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            IContainer container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
