namespace EventBus.Messages.Events;

public class OrderCreatedIntegrationEvent : IntegrationEvent
{
    public Guid OrderId { get; }
    public decimal TotalAmount { get; }

    public OrderCreatedIntegrationEvent(Guid orderId, decimal totalAmount)
    {
        OrderId = orderId;
        TotalAmount = totalAmount;
    }
}