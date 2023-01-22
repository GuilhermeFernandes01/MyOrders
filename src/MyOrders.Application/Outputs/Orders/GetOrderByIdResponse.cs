using MyOrders.Domain.Models;

namespace MyOrders.Application.Orders.Outputs
{
	public class GetOrderByIdResponse
	{
		public Order Order { get; }

		public GetOrderByIdResponse(Order order)
		{
			Order = order;
		}
    }
}