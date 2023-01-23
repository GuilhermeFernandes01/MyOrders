using Microsoft.Extensions.Logging;
using MyOrders.Application.Orders.Outputs;
using MyOrders.Domain.Persistence;

namespace MyOrders.Application.UseCases.Orders.GetAll;

public class GetOrdersUseCase : IGetOrdersUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GetOrdersUseCase> _logger;

    public GetOrdersUseCase(IOrderRepository orderRepository, ILogger<GetOrdersUseCase> logger)
	{
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<GetAllOrdersResponse> Execute(CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersAsync(cancellationToken).ConfigureAwait(false);
        
        _logger.LogInformation("DB_RESPONSE: {orders}", orders);
        
        var response = new GetAllOrdersResponse(orders.Count(), orders);

        return response;
	}
}