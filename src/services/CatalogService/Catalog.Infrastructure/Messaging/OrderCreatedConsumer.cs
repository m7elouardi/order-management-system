using System.Text;
using System.Text.Json;
using EventBus.Messages.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Catalog.Infrastructure.Messaging;

public class OrderCreatedConsumer
{
    private readonly IConnection _connection;
    private IChannel? _channel;

    public OrderCreatedConsumer(IConnection connection)
    {
        _connection = connection;
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _channel = await _connection.CreateChannelAsync(
            options: null,
            cancellationToken: cancellationToken);

        await _channel.ExchangeDeclareAsync(
            exchange: "event_bus",
            type: ExchangeType.Fanout,
            cancellationToken: cancellationToken);

        var queue = await _channel.QueueDeclareAsync(
            cancellationToken: cancellationToken);

        await _channel.QueueBindAsync(
            queue: queue.QueueName,
            exchange: "event_bus",
            routingKey: "",
            cancellationToken: cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync += HandleEventAsync;

        await _channel.BasicConsumeAsync(
            queue: queue.QueueName,
            autoAck: true,
            consumer: consumer,
            cancellationToken: cancellationToken);
    }

    private async Task HandleEventAsync(
        object sender,
        BasicDeliverEventArgs eventArgs)
    {
        var message = Encoding.UTF8.GetString(eventArgs.Body.ToArray());

        var orderCreated =
            JsonSerializer.Deserialize<OrderCreatedIntegrationEvent>(message);

        Console.WriteLine(
            $"📦 Order received in CatalogService: {orderCreated!.OrderId}");

        await Task.CompletedTask;
    }
}
