using MyOrders.Application.Orders.Inputs;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Domain.Exceptions;
using MyOrders.Domain.Persistence;

namespace MyOrders.Application.UseCases.Order.Create
{
	public class CreateOrderUseCase : ICreateOrderUseCase
    {
		private readonly IOrderRepository _orderRepository;
		private readonly IUnitOfWork _unitOfWork;

        public CreateOrderUseCase(IOrderRepository orderRepository, IUnitOfWork unitOfWork) {
			_orderRepository = orderRepository;
			_unitOfWork = unitOfWork;
        }

		public async Task<CreateOrderResponse> Execute(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken)
		{
            ValidateRequest(createOrderRequest);

			var order = new Domain.Models.Order
			{
				ProductName = createOrderRequest.ProductName,
				Quantity = createOrderRequest.Quantity,
				Paid = false,
				Shipped = false,
			};

			await _orderRepository.AddOrUpdateAsync(order, cancellationToken);
			await _unitOfWork.SaveEntitiesAsync(cancellationToken);

			var response = new CreateOrderResponse
			{
				OrderId = order.OrderId
			};

			return response;
        }

		private static void ValidateRequest(CreateOrderRequest createOrderRequest)
		{
			var validator = new CreateOrderValidator();
			var validationResult = validator.Validate(createOrderRequest);

			if (!validationResult.IsValid)
			{
				var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
				throw new ValidationErrorsException(errorMessages);
            }
		}
	}
}

