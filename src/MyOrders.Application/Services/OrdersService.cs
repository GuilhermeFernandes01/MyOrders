using MyOrders.Domain.Persistence;
using MyOrders.Domain.Models;

namespace MyOrders.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order?> MarkOrderAsPaidAsync(int orderId)
        {
            return await _orderRepository.MarkOrderAsPaidAsync(orderId);
        }

        public async Task<Order?> MarkOrderAsShippedAsync(int orderId)
        {
            return await _orderRepository.MarkOrderAsShippedAsync(orderId);
        }
    }
}