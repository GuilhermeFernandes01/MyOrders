using MyOrders.Domain.Models;

namespace MyOrders.Application.Outputs.Orders
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