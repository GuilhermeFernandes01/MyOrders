namespace MyOrders.Application.DTOs.Orders;

public class CreateOrderDto
{
    public string ProductName { get; }

    public int Quantity { get; }

    public CreateOrderDto(string productName, int quantity)
	{
        ProductName = !String.IsNullOrWhiteSpace(productName) ? productName : throw new ArgumentException();
        Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException();
	}
}