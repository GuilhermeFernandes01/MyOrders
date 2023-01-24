using MyOrders.Domain.Models;

namespace MyOrders.Application.Outputs.Orders;

public class GetAllOrdersResponse
{
    public int Total { get; }

    public IEnumerable<Order> Orders { get; }

    public GetAllOrdersResponse(int total, IEnumerable<Order> orders)
    {
        Total = total;
        Orders = orders;
    }
}