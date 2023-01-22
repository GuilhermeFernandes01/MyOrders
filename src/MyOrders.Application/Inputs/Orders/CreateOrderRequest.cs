namespace MyOrders.Application.Orders.Inputs
{
	public class CreateOrderRequest
	{
		public string ProductName { get; set; }

		public int Quantity { get; set; }
	}
}