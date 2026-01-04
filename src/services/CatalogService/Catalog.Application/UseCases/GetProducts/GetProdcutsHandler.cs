using MediatR;
using Catalog.Application.DTOs;
using Catalog.Domain.Interfaces;

namespace Catalog.Application.UseCases.GetProducts;

public class GetProductsHandler: IRequestHandler<GetProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProductDto>> Handle(
        GetProductsQuery request, 
        CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();

        return products.Select(p =>
            new ProductDto(p.Id, p.Name, p.Price));
    }

}