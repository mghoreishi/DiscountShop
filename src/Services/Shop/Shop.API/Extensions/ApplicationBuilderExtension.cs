namespace Shopping.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// use swagger
        /// </summary>
        /// <param name="app"></param>
        public static void UseCustomSwagger(this IApplicationBuilder app, string pathBase)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Inventory API V1"));
        }


    }
}
