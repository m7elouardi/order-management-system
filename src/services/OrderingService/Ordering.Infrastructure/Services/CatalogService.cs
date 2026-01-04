using System.Net.Http.Json;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;

namespace Ordering.Infrastructure.Services;

public class CatalogService : ICatalogService
{
    private readonly HttpClient _httpClient;

    public CatalogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CatalogProductDto?> GetProductById(Guid productId)
    {
        return await _httpClient
            .GetFromJsonAsync<CatalogProductDto>($"api/products/{productId}");
    }
}