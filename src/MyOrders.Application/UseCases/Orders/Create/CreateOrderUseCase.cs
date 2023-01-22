using MyOrders.Application.Orders.Inputs;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Domain.Exceptions;
using MyOrders.Domain.Persistence;
using MyOrders.Domain.Models;

namespace MyOrders.Application.UseCases.Orders.Create
{
	public class CreateOrderUseCase : ICreateOrderUseCase
    {
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;

        public CreateOrderUseCase(IOrderRepository orderRepository, IUnitOfWork unitOfWork) {
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
        }

		public async Task<CreateOrderResponse> Execute(CreateOrderDTO createOrderDTO, CancellationToken cancellationToken)
		{
			var order = new Order
			{
				ProductName = createOrderDTO.ProductName,
				Quantity = createOrderDTO.Quantity,
				Paid = false,
				Shipped = false
			};

			await _orderRepository.AddOrUpdateAsync(order, cancellationToken).ConfigureAwait(false);

			var response = new CreateOrderResponse(order.OrderId);

			return response;
        }
	}
}

