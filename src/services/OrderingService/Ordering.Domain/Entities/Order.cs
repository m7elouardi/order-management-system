using Ordering.Domain.Enums;

namespace Ordering.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public OrderStatus Status { get; private set; }
    public Guid CustomerId { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public decimal TotalAmount { get; private set; }

    private Order() { } // For EF

    public Order(Guid customerId, IEnumerable<OrderItem> items)
    {
        if (items == null || !items.Any())
            throw new ArgumentException("Order must contain at least one item");

        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Pending;

        _items.AddRange(items);
        RecalculateTotal();
    }

    public void AddItem(Guid productId, string productName, decimal price, int quantity)
    {
        _items.Add(new OrderItem(productId, productName, price, quantity));
        RecalculateTotal();
    }

    private void RecalculateTotal()
    {
        TotalAmount = _items.Sum(i => i.Price * i.Quantity);
    }
}
