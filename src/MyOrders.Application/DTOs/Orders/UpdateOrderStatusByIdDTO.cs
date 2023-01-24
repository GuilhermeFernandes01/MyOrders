namespace MyOrders.Application.DTOs.Orders;

public class UpdateOrderStatusByIdDto
{
	public int OrderId { get; }

	public UpdateOrderStatusByIdDto(int? orderId)
	{
		OrderId = orderId.HasValue && orderId.Value > 0
			? orderId.Value
			: throw new ArgumentNullException(nameof(orderId));
	}
}