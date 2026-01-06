using Catalog.Domain.Interfaces;
using Catalog.Infrastructure.Messaging;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

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

// -------------------------------
        // Register RabbitMQ Connection
        // -------------------------------
        services.AddSingleton<IConnection>(sp =>
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq" // Replace with your RabbitMQ hostname
            };
            
            // Note: .CreateConnectionAsync returns Task<IConnection>, we need sync version here
            return (IConnection)factory.CreateConnectionAsync();
        });

        // -------------------------------
        // Register Consumer
        // -------------------------------
        services.AddSingleton<OrderCreatedConsumer>();

        
        return services;
    }
}