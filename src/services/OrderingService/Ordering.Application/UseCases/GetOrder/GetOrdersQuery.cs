using MediatR;
using Ordering.Application.DTOs;
namespace Ordering.Application.UseCases.GetOrders;

public record GetOrdersQuery(Guid CustomerId) : IRequest<IEnumerable<OrderDto>>;