using Microsoft.Extensions.Logging;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Domain.Exceptions;
using MyOrders.Domain.Persistence;
using MyOrders.Domain.Models;

namespace MyOrders.Application.UseCases.Orders.Create
{
	public class CreateOrderUseCase : ICreateOrderUseCase
    {
		private readonly IOrderRepository _orderRepository;
		private readonly ILogger<CreateOrderUseCase> _logger;
		private readonly IUnitOfWork _unitOfWork;

        public CreateOrderUseCase(
			IOrderRepository orderRepository,
			ILogger<CreateOrderUseCase> logger,
			IUnitOfWork unitOfWork)
		{
			_orderRepository = orderRepository;
			_logger = logger;
			_unitOfWork = unitOfWork;
        }

		public async Task<CreateOrderResponse> Execute(CreateOrderDto CreateOrderDto, CancellationToken cancellationToken)
		{
			var order = new Order
			{
				ProductName = CreateOrderDto.ProductName,
				Quantity = CreateOrderDto.Quantity,
				Paid = false,
				Shipped = false
			};

			await _orderRepository.AddOrUpdateAsync(order, cancellationToken).ConfigureAwait(false);

			_logger.LogInformation("INFO: Order created sucessfully {order}", order);

			var response = new CreateOrderResponse(order.OrderId);

			return response;
        }
	}
}

