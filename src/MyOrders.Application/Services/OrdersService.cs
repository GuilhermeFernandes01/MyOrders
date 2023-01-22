using MyOrders.Domain.Persistence;

namespace MyOrders.Application.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task MarkOrderAsPaidAsync(int orderId)
        {
            await _orderRepository.MarkOrderAsPaidAsync(orderId);
        }

        public async Task MarkOrderAsShippedAsync(int orderId)
        {
            await _orderRepository.MarkOrderAsShippedAsync(orderId);
        }

    }
}