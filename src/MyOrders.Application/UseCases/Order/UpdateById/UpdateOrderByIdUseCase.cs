using MassTransit;
using MassTransit.Transports;
using System.Net;
using Microsoft.Extensions.Logging;
using MyOrders.Application.Orders.Inputs;
using MyOrders.Domain.Exceptions;
using MyOrders.Domain.Persistence;
using MyOrders.Domain.Contracts;

namespace MyOrders.Application.UseCases.Order.UpdateById
{
	public class UpdateOrderByIdUseCase : IUpdateOrderByIdUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IBus _bus;
        private readonly ILogger<UpdateOrderByIdUseCase> _logger;

        public UpdateOrderByIdUseCase(
            IOrderRepository orderRepository,
            ILogger<UpdateOrderByIdUseCase> logger,
            IBus bus)
		{
            _orderRepository = orderRepository;
            _bus = bus;
            _logger = logger;
        }

		public async Task Execute(UpdateOrderStatusByIdRequest updateOrderStatusByIdRequest, CancellationToken cancellationToken)
		{
            var orderExists = await _orderRepository.CheckOrderIdExists(updateOrderStatusByIdRequest.OrderId, cancellationToken);

            ValidateRequest(orderExists);

            var orderToBeSent = new OrderPaymentConfirmedMessage
            {
                OrderId = updateOrderStatusByIdRequest.OrderId
            };

            await _bus.Publish(orderToBeSent, cancellationToken);
        }

		private void ValidateRequest(bool orderExists)
        {
            if (!orderExists)
            {
                var orderNotFoundMessage = "Order not found";

                _logger.LogError("ERROR: {response}", orderNotFoundMessage);

                throw new NotFoundException(orderNotFoundMessage);
            }
        }
	}
}

