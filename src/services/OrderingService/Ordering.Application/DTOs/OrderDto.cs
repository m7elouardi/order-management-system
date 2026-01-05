namespace Ordering.Application.DTOs;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    DateTime CreatedAt,
    decimal TotalAmount
);
