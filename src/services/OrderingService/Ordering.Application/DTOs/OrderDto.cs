namespace Ordering.Application.DTOs;

public record OrderDto(
    Guid Id,
    DateTime CreatedAt,
    decimal TotalAmount
);
