namespace Ordering.Application.DTOs;

public record CatalogProductDto(
    Guid Id,
    string Name,
    decimal Price
);
