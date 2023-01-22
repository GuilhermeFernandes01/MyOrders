namespace MyOrders.Domain.Contracts
{
    public class OrderPaymentConfirmedMessage
    {
        public OrderPaymentConfirmedMessage()
        {
        }

        public OrderPaymentConfirmedMessage(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}