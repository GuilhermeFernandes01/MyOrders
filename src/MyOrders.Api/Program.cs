using MyOrders.Api.Filters;
using MyOrders.Application;
using MyOrders.Domain.Contracts;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.InjectDependencies(builder.Configuration);
        builder.Services.AddRabbitConfig(
            configuration: builder.Configuration,
            rabbitConfig: (cfg) => cfg.Publish<OrderPaymentConfirmedMessage>()
        );

        builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddLogging();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}