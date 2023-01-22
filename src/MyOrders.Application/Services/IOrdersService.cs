namespace MyOrders.Application.Services
{
    public interface IOrdersService
    {
        Task MarkOrderAsPaidAsync(int orderId);

        Task MarkOrderAsShippedAsync(int orderId);
    }
}