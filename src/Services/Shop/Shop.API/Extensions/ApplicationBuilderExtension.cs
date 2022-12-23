using EventBus;
using Shopping.API.Application.IntegrationEvents.EventHandlers;
using Shopping.API.Application.IntegrationEvents.Events;

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
            app.UseSwaggerUI(c => c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty) }/swagger/v1/swagger.json", "Shop API V1"));
        }

        /// <summary>
        /// Event Bus Subscribtion
        /// </summary>
        /// <param name="app"></param>
        public static void AddSubscribtions(this IApplicationBuilder app)
        {
            IEventBus eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<IncreaseDiscountCountIntegrationEvent, IncreaseDiscountCountIntegrationEventHandler>();
           

        }
    }
}
