using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyOrders.Application.UseCases.Orders.Create;
using MyOrders.Application.UseCases.Orders.GetAll;
using MyOrders.Application.UseCases.Orders.GetById;
using MyOrders.Application.UseCases.Orders.UpdateById;
using MyOrders.Domain.Persistence;
using MyOrders.Infrastructure;
using MyOrders.Infrastructure.Repositories;

namespace MyOrders.Application;

public static class DependencyInjection
{
    private static IConfiguration _configuration;

		public static IServiceCollection InjectDependencies(this IServiceCollection services, IConfiguration configuration)
		{
        _configuration = configuration;
        var connectionString = _configuration.GetSection("SqlServerSettings:ConnectionString").Value;

        services.AddDbContext<MyOrdersDbContext>(optionsAction: options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        AddUseCases(services);

        return services;
		}

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        services.AddScoped<IGetOrdersUseCase, GetOrdersUseCase>();
        services.AddScoped<IUpdateOrderByIdUseCase, UpdateOrderByIdUseCase>(); 
    }

    public static IServiceCollection AddRabbitConfig(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator>? busConfig = null,
        Action<IRabbitMqBusFactoryConfigurator>? rabbitConfig = null
    )
    {
        _configuration = configuration;
        var rabbitHost = _configuration.GetSection("RabbitMqSettings:Host").Value;
        var rabbitUsername = _configuration.GetSection("RabbitMqSettings:Username").Value;
        var rabbitPassword = _configuration.GetSection("RabbitMqSettings:Password").Value;

        services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();

            busConfig?.Invoke(x);

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.UseDelayedMessageScheduler();
                cfg.UseMessageRetry(retry => retry.Intervals(30000, 60000, 900000));
                cfg.ConfigureEndpoints(context);
                cfg.Host(rabbitHost, host =>
                {
                    host.Username(rabbitUsername);
                    host.Password(rabbitPassword);
                });

                rabbitConfig?.Invoke(cfg);
            });
        });

        return services;
    }
	}

