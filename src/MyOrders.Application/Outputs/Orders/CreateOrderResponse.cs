using MyOrders.Domain.Models;

namespace MyOrders.Application.Orders.Outputs
{
    public class CreateOrderResponse
    {
        public int OrderId { get; }

        public CreateOrderResponse(int orderId)
        {
            OrderId = orderId;
        }
    }
}