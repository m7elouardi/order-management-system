using MediatR;
using Ordering.Application.DTOs;

namespace Ordering.Application.UseCases.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    List<CreateOrderItemDto> Items
) : IRequest<Guid>;


