namespace MyOrders.Application.DTOs.Orders;

public class GetOrderByIdDTO
{
    public int OrderId { get; }

    public GetOrderByIdDTO(int? orderId)
    {
        OrderId = orderId.HasValue && orderId.Value > 0 ? orderId.Value : throw new ArgumentNullException(nameof(orderId));
    }
}