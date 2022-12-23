using Autofac;
using IntegrationEventLogEF.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EventBus;
using RabbitMQ.Client;
using System;
using System.Data.Common;
using System.Linq;

namespace EventBusRabbitMQ
{
    public static class EventBusCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, EventBusOptions eventBusOptions)
        {
            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                string subscriptionClientName = eventBusOptions.SubscriptionClientName;
                IRabbitMQPersistentConnection rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                ILifetimeScope iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                ILogger<EventBusRabbitMQ> logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                IEventBusSubscriptionsManager eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                int retryCount = 5;
                if (eventBusOptions.EventBusRetryCount > 0)
                {
                    retryCount = eventBusOptions.EventBusRetryCount;
                }

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, EventBusOptions eventBusOptions, string entryAssembly = "")
        {
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c, entryAssembly));

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                ILogger<DefaultRabbitMQPersistentConnection> logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                var host_port = eventBusOptions.EventBusConnection.Split(":");
                ConnectionFactory factory = new()
                {
                    HostName = host_port.FirstOrDefault() ?? "localhost",
                    DispatchConsumersAsync = true
                };

                if (host_port.Length == 2)
                {
                    factory.Port = int.Parse(host_port[1]);
                }

                if (!string.IsNullOrEmpty(eventBusOptions.EventBusUserName))
                {
                    factory.UserName = eventBusOptions.EventBusUserName;
                }

                if (!string.IsNullOrEmpty(eventBusOptions.EventBusPassword))
                {
                    factory.Password = eventBusOptions.EventBusPassword;
                }

                int retryCount = 5;
                if (eventBusOptions.EventBusRetryCount > 0)
                {
                    retryCount = eventBusOptions.EventBusRetryCount;
                }

                return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
            });

            return services;
        }
    }
}
