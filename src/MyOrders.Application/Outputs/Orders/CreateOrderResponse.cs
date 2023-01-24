using MyOrders.Domain.Models;

namespace MyOrders.Application.Outputs.Orders;

public class CreateOrderResponse
{
    public int OrderId { get; }

    public CreateOrderResponse(int orderId)
    {
        OrderId = orderId;
    }
}