using Microsoft.Extensions.Logging;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Outputs.Orders;
using MyOrders.Domain.Models;
using MyOrders.Domain.Exceptions;
using MyOrders.Domain.Persistence;
using System.Text.Json;

namespace MyOrders.Application.UseCases.Orders.GetById;

public class GetOrderByIdUseCase : IGetOrderByIdUseCase
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<GetOrderByIdUseCase> _logger;

    public GetOrderByIdUseCase(IOrderRepository orderRepository, ILogger<GetOrderByIdUseCase> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<GetOrderByIdResponse> Execute(GetOrderByIdDto GetOrderByIdDto, CancellationToken cancellationToken)
    {
        var order = await _orderRepository
            .GetOrderByIdAsync(GetOrderByIdDto.OrderId, cancellationToken)
            .ConfigureAwait(false);
        
        _logger.LogInformation("DB_RESPONSE: {orders}", JsonSerializer.Serialize(order));

        ValidateRequest(order);

        var response = new GetOrderByIdResponse(order!);

        return response;
    }

    private void ValidateRequest(Order? order)
    {
        if (order == null)
        {
            var orderNotFoundMessage = "Order not found";

            _logger.LogError("ERROR: {response}", orderNotFoundMessage);

            throw new NotFoundException(orderNotFoundMessage);
        }
    }
}