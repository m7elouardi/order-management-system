using MediatR;
using Catalog.Application.DTOs;
namespace Catalog.Application.UseCases.GetProducts;

public record GetProductsQuery : IRequest<IEnumerable<ProductDto>>;