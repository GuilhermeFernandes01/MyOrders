using MyOrders.Domain.Models;

namespace MyOrders.Application.Services;

public interface IOrdersService
{
    Task<Order?> MarkOrderAsPaidAsync(int orderId);

    Task<Order?> MarkOrderAsShippedAsync(int orderId);
}