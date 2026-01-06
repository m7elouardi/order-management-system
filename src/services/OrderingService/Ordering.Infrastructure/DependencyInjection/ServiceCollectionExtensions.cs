using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Domain.Interfaces;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;
using Ordering.Infrastructure.Services;
using Ordering.Application.Interfaces;
using Ordering.Infrastructure.Messaging;
using RabbitMQ.Client;

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
        
        // -------------------------------
        // Register RabbitMQ
        // -------------------------------
        services.AddSingleton<IConnection>(sp =>
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq" // replace with your RabbitMQ hostname
            };
            return (IConnection)factory.CreateConnectionAsync();
        });
        services.AddScoped<IEventBus, RabbitMqEventBus>();
        
        return services;
    }
}