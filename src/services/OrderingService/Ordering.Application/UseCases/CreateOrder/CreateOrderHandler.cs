using MediatR;
using Ordering.Domain.Entities;
using Ordering.Domain.Interfaces;
using Ordering.Application.Interfaces;
using EventBus.Messages.Events;

namespace Ordering.Application.UseCases.CreateOrder;

public class CreateOrderHandler 
    : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICatalogService _catalogService;
    private readonly IEventBus _eventBus;

    public CreateOrderHandler(
        IOrderRepository orderRepository,
        ICatalogService catalogService,
        IEventBus eventBus)
    {
        _orderRepository = orderRepository;
        _catalogService = catalogService;
        _eventBus = eventBus;
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
                product.Name,
                product.Price,
                item.Quantity
            ));
        }

        // ✅ Domain decides CreatedAt & TotalAmount
        var order = new Order(
            command.CustomerId,
            items);

        await _orderRepository.AddAsync(order);
        _eventBus.Publish(new OrderCreatedIntegrationEvent(order.Id, order.TotalAmount));

        return order.Id;
    }
}
