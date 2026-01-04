using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Interfaces;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Services;
using Ordering.Application.Interfaces;

namespace Ordering.Infrastructure.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddHttpClient<ICatalogService, CatalogService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
        });
        
        return services;
    }
}