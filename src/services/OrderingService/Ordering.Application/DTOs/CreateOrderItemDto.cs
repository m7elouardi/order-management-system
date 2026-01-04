namespace Ordering.Application.DTOs;
public record CreateOrderItemDto(
    Guid ProductId,
    string ProductName,
    decimal Price,
    int Quantity
);