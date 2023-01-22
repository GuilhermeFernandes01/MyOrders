using MyOrders.Domain.Models;

namespace MyOrders.Application.Orders.Outputs
{
    public class GetAllOrdersResponse
    {
        public int Total { get; set; }

        public IEnumerable<Order> Orders { get; set; } = new List<Order>();
    }
}