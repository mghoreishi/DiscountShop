namespace EventBus
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler where TIntegrationEvent : IntegrationEvent
    {
        System.Threading.Tasks.Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler { }
}
