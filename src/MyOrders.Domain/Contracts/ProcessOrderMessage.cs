namespace MyOrders.Domain.Contracts;

public class ProcessOrderMessage
{
    public int OrderId { get; }

    public ProcessOrderMessage(int orderId)
    {
        OrderId = orderId;
    }
}