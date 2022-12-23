using EventBus;
using System;
using System.Threading.Tasks;

namespace Discounting.API.Application.IntegrationEvents
{
    public interface IDiscountingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
