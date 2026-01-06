using EventBus.Messages.Events;

namespace Ordering.Application.Interfaces;

public interface IEventBus
{
    void Publish(IntegrationEvent @event);
}