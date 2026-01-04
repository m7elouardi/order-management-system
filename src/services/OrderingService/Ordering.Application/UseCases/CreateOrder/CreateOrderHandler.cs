using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.Interfaces;
using Ordering.Application.Interfaces;

namespace Ordering.Application.UseCases.CreateOrder;

public class CreateOrderHandler 
    : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICatalogService _catalogService;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        ICatalogService catalogService)
    {
        _orderRepository = orderRepository;
        _catalogService = catalogService;
    }

    public async Task<Guid> Handle(
        CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var items = new List<OrderItem>();

        foreach (var item in command.Items)
        {
            var product = await _catalogService
                .GetProductById(item.ProductId);

            if (product is null)
            {
                throw new ApplicationException(
                    $"Product {item.ProductId} does not exist.");
            }

            items.Add(new OrderItem(
                product.Id,
                product.Price,
                item.Quantity
            ));
        }

        // ✅ Domain decides CreatedAt & TotalAmount
        var order = new Order(items);

        await _orderRepository.AddAsync(order);
        return order.Id;
    }
}
