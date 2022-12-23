using System.Threading.Tasks;

namespace EventBus
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
