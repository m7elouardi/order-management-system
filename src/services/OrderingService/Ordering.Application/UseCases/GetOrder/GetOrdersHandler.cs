using MediatR;
using Ordering.Application.DTOs;
using Ordering.Domain.Interfaces;

namespace Ordering.Application.UseCases.GetOrders;

public class GetOrdersHandler
    : IRequestHandler<GetOrdersQuery, IEnumerable<OrderDto>>
{
    private readonly IOrderRepository _repository;

    public GetOrdersHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<OrderDto>> Handle(
        GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await _repository.GetAllAsync();

        return orders.Select(o =>
            new OrderDto(
                o.Id,
                o.CreatedAt,
                o.TotalAmount
            ));
    }
}
