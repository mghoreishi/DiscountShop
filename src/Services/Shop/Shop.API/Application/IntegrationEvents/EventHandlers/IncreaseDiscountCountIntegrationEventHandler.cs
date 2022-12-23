using MediatR;
using EventBus;
using Shopping.API.Application.Commands.IncreaseDiscountCount;
using Shopping.API.Application.IntegrationEvents.Events;

namespace Shopping.API.Application.IntegrationEvents.EventHandlers
{
    public class IncreaseDiscountCountIntegrationEventHandler : IIntegrationEventHandler<IncreaseDiscountCountIntegrationEvent>
    {
        private readonly IMediator _mediator;

        public IncreaseDiscountCountIntegrationEventHandler(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task Handle(IncreaseDiscountCountIntegrationEvent @event)
        {
            await _mediator.Send(new IncreaseDiscountCountCommand(@event.ShopId));
        }
    }
}
