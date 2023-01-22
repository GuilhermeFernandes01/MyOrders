using MyOrders.Application;
using MyOrders.Application.Services;
using MyOrders.OrderProcessingWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IOrdersService, OrdersService>();
        services.InjectDependencies(services.BuildServiceProvider().GetRequiredService<IConfiguration>());
        services.AddRabbitConfig(
            services.BuildServiceProvider().GetRequiredService<IConfiguration>(),
            (x) => x.AddConsumer<ProcessShipmentOrder>().Endpoint(x => x.Name = "confirm-order-shipment-queue")
        );
    })
    .Build();

await host.RunAsync();