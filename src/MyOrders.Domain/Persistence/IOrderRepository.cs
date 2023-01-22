using MyOrders.Domain.Models;

namespace MyOrders.Domain.Persistence
{
    public interface IOrderRepository
    {
        Task<int> AddOrUpdateAsync(Order order, CancellationToken cancellationToken);

        Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);

        Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken);

        Task<bool> CheckOrderIdExists(int orderId, CancellationToken cancellationToken);

        Task MarkOrderAsPaidAsync(int orderId);

        Task MarkOrderAsShippedAsync(int orderId);
    }
}