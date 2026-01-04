namespace Ordering.Application.DTOs;

public record CreateOrderDto(
    List<CreateOrderItemDto> Items
);