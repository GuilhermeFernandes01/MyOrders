using MyOrders.Application;
using MyOrders.Application.Services;
using MyOrders.Domain.Contracts;
using MyOrders.OrderPaymentConfirmationWorker;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IOrdersService, OrdersService>();
        services.InjectDependencies(services.BuildServiceProvider().GetRequiredService<IConfiguration>());
        services.AddRabbitConfig(
            services.BuildServiceProvider().GetRequiredService<IConfiguration>(),
            (x) => x.AddConsumer<ProcessPaymentOrder>().Endpoint(x => x.Name = "confirm-order-payment-queue"),
            (cfg) => cfg.Publish<ProcessOrderMessage>()
        );
    })
    .Build();

await host.RunAsync();