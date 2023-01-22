using MassTransit;
using MyOrders.Domain.Contracts;
using MyOrders.Application.Services;
using System.Text.Json;

namespace MyOrders.OrderProcessingWorker
{
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
            _logger.LogInformation("EVENT: Message received {message}", JsonSerializer.Serialize(context.Message));

            var message = context.Message;
            await _ordersService.MarkOrderAsShippedAsync(message.OrderId);

            _logger.LogInformation("SUCCESS: Message processed successfully {message}", JsonSerializer.Serialize(context.Message));
        }
    }
}