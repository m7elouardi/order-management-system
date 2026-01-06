using System.Text;
using System.Text.Json;
using EventBus.Messages.Events;
using Ordering.Application.Interfaces;
using RabbitMQ.Client;

namespace Ordering.Infrastructure.Messaging;

public class RabbitMqEventBus : IEventBus
{
    private readonly IConnection _connection;

    public RabbitMqEventBus(IConnection connection)
    {
        _connection = connection;
    }

    public async void Publish(IntegrationEvent @event)
    {
    {
        await using var channel = await _connection.CreateChannelAsync();

        await channel.ExchangeDeclareAsync(
            exchange: "event_bus",
            type: ExchangeType.Fanout);

        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(
            exchange: "event_bus",
            routingKey: "",
            body: body);
    }
}
}
