namespace MyOrders.Application.Inputs.Orders
{
	public class CreateOrderRequest
	{
		public string ProductName { get; set; }

		public int Quantity { get; set; }
	}
}