namespace MyOrders.Domain.Contracts;

public class OrderPaymentConfirmedMessage
{
    public int OrderId { get; }

    public OrderPaymentConfirmedMessage(int orderId)
    {
        OrderId = orderId;
    }
}