namespace MyOrders.Domain.Contracts
{
    public class ProcessOrderMessage
    {
        public ProcessOrderMessage()
        {
        }

        public ProcessOrderMessage(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}