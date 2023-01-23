namespace MyOrders.Application.DTOs.Orders;

public class GetOrderByIdDto
{
    public int OrderId { get; }

    public GetOrderByIdDto(int? orderId)
    {
        OrderId = orderId.HasValue && orderId.Value > 0 ? orderId.Value : throw new ArgumentNullException(nameof(orderId));
    }
}