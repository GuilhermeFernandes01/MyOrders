using MassTransit;
using MyOrders.Domain.Contracts;
using MyOrders.Application.Services;
using System.Text.Json;

namespace MyOrders.OrderPaymentConfirmationWorker
{
	public class ProcessPaymentOrder : IConsumer<OrderPaymentConfirmedMessage>
	{
        private readonly IOrdersService _ordersService;
        private readonly ILogger<ProcessPaymentOrder> _logger;
        private readonly IBus _bus;

        public ProcessPaymentOrder(IOrdersService ordersService, IBus bus, ILogger<ProcessPaymentOrder> logger)
		{
            _ordersService = ordersService;
            _logger = logger;
            _bus = bus;
        }

        public async Task Consume(ConsumeContext<OrderPaymentConfirmedMessage> context)
        {
            _logger.LogInformation("EVENT: Message received {message}", JsonSerializer.Serialize(context.Message));

            var message = context.Message;
            await _ordersService.MarkOrderAsPaidAsync(message.OrderId);

            _logger.LogInformation("SUCCESS: Order paid successfully {message}", JsonSerializer.Serialize(context.Message));

            var messageToBeSent = new ProcessOrderMessage
            {
                OrderId = context.Message.OrderId
            };

            await _bus.Publish(messageToBeSent);
            
            _logger.LogInformation("SUCCESS: Message sent successfully {message}", JsonSerializer.Serialize(messageToBeSent));
        }
    }
}
