namespace Shopping.API.Extensions
{
    public static class SwaggerGenServiceCollectionExtensions
    {
        /// <summary>
        /// Add Custom Swagger
        /// </summary>
        /// <param name="services">Specifies the contract for a collection of service descriptors</param>
        public static void AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Shop API", Version = "v1" });

            });
        }
    }
}
