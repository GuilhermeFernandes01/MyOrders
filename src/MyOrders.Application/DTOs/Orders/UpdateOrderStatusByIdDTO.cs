namespace MyOrders.Application.DTOs.Orders;

public class UpdateOrderStatusByIdDTO
{
	public int OrderId { get; }

	public UpdateOrderStatusByIdDTO(int? orderId)
	{
		OrderId = orderId.HasValue && orderId.Value > 0 ? orderId.Value : throw new ArgumentNullException(nameof(orderId));
	}
}