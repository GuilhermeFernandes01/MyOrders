using MassTransit;
using MyOrders.Domain.Contracts;
using MyOrders.Application.Services;
using System.Text.Json;
using MyOrders.Domain.Exceptions;

namespace MyOrders.OrderProcessingWorker;

public class ProcessShipmentOrder : IConsumer<ProcessOrderMessage>
{
    private readonly IOrdersService _ordersService;
    private readonly ILogger<ProcessShipmentOrder> _logger;

    public ProcessShipmentOrder(IOrdersService ordersService, ILogger<ProcessShipmentOrder> logger)
    {
        _ordersService = ordersService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProcessOrderMessage> context)
    {
        try
        {
            _logger.LogInformation("EVENT: Message received {message}", JsonSerializer.Serialize(context.Message));

            var message = context.Message;
            var updatedOrder = await _ordersService.MarkOrderAsShippedAsync(message.OrderId);

            if (updatedOrder == null || !updatedOrder.Shipped)
            {
                throw new OrderUpdateFailedException();
            }

            _logger.LogInformation("SUCCESS: Message processed successfully {message}", JsonSerializer.Serialize(context.Message));
        } catch (Exception ex)
        {
            _logger.LogError("ERROR: An error ocurred {message} {erorrMessage} {error}", JsonSerializer.Serialize(context.Message), ex.Message, ex);
        }
    }
}