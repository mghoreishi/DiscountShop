using IntegrationEventLogEF;
using EventBus;

namespace Discounting.API.Application.IntegrationEvents.Events
{
    public record IncreaseDiscountCountIntegrationEvent: IntegrationEvent
    {
        public IncreaseDiscountCountIntegrationEvent(long shopId)
        {
            ShopId = shopId;
        }

        public long ShopId { get; init; }
    }
}
