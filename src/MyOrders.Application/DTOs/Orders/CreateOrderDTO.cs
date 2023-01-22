using FluentValidation;

public class CreateOrderDTO
{
    public string ProductName { get; }

    public int Quantity { get; }

    public CreateOrderDTO(string productName, int quantity)
	{
        ProductName = !String.IsNullOrWhiteSpace(productName) ? productName : throw new ArgumentException();
        Quantity = quantity > 0 ? quantity : throw new ArgumentOutOfRangeException();
	}
}