using MyOrders.Domain.Models;

namespace MyOrders.Domain.Persistence;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);

    Task<Order?> GetOrderByIdAsync(int orderId, CancellationToken cancellationToken);

    Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken);

    Task<bool> CheckOrderIdExists(int orderId, CancellationToken cancellationToken);

    Task<Order?> MarkOrderAsPaidAsync(int orderId);

    Task<Order?> MarkOrderAsShippedAsync(int orderId);
}