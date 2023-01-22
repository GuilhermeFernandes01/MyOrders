namespace MyOrders.Application.Orders.Inputs
{
	public class CreateOrderRequest
	{
		public string ProductName { get; set; } = string.Empty;

		public int Quantity { get; set; }
	}
}