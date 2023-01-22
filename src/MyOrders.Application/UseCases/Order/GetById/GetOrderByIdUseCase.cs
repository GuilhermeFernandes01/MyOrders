using Microsoft.Extensions.Logging;
using MyOrders.Application.Orders.Inputs;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Domain.Exceptions;
using MyOrders.Domain.Persistence;

namespace MyOrders.Application.UseCases.Order.GetById
{
	public class GetOrderByIdUseCase : IGetOrderByIdUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrderByIdUseCase> _logger;

        public GetOrderByIdUseCase(IOrderRepository orderRepository, ILogger<GetOrderByIdUseCase> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<GetOrderByIdResponse> Execute(GetOrderByIdRequest getOrderByIdRequest, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(getOrderByIdRequest.OrderId, cancellationToken);

            ValidateRequest(order);

            var response = new GetOrderByIdResponse
            {
                Order = order
            };

            return response;
        }

        private void ValidateRequest(Domain.Models.Order? order)
        {
            if (order == null)
            {
                var orderNotFoundMessage = "Order not found";

                _logger.LogError("ERROR: {response}", orderNotFoundMessage);

                throw new NotFoundException(orderNotFoundMessage);
            }
        }
    }
}

