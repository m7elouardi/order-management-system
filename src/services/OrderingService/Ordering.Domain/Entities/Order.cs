namespace Ordering.Domain.Entities;

public class Order
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public decimal TotalAmount { get; private set; }

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    // Constructor EF Core
    private Order() { }

    public Order(IEnumerable<OrderItem> items)
    {
        if (items == null || !items.Any())
            throw new ArgumentException("Order must contain at least one item");

        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;

        _items.AddRange(items);
        TotalAmount = _items.Sum(i => i.Price * i.Quantity);
    }
}
