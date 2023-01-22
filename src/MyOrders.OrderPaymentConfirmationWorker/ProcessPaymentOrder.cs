using MassTransit;
using System.Text.Json;
using MyOrders.Application.Services;
using MyOrders.Domain.Contracts;
using MyOrders.Domain.Exceptions;

namespace MyOrders.OrderPaymentConfirmationWorker;

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
        try
        {
            _logger.LogInformation("EVENT: Message received {message}", JsonSerializer.Serialize(context.Message));

            var message = context.Message;
            var updatedOrder = await _ordersService.MarkOrderAsPaidAsync(message.OrderId);

            if (updatedOrder == null || !updatedOrder.Paid)
            {
                throw new OrderUpdateFailedException();
            }

            _logger.LogInformation("SUCCESS: Order paid successfully {message}", JsonSerializer.Serialize(context.Message));

            var messageToBeSent = new ProcessOrderMessage(context.Message.OrderId);

            await _bus.Publish(messageToBeSent);

            _logger.LogInformation("SUCCESS: Message sent successfully {message}", JsonSerializer.Serialize(messageToBeSent));
        } catch (Exception ex)
        {
            _logger.LogError("ERROR: An error ocurred {message} {erorrMessage} {error}", JsonSerializer.Serialize(context.Message), ex.Message, ex);
        }
    }
}
