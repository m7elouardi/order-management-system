using Catalog.Domain.Interfaces;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<CatalogDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}