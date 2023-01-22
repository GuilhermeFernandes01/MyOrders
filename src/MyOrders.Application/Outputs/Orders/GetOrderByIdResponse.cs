using MyOrders.Domain.Models;

namespace MyOrders.Application.Orders.Outputs
{
	public class GetOrderByIdResponse
	{
		public Order Order { get; set; }
    }
}