using EventBus;

namespace Shopping.API.Application.IntegrationEvents
{
    public interface IShoppingIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);
    }
}
