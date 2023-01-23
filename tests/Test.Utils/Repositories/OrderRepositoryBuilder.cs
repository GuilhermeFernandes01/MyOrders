using MassTransit.Transports;
using Moq;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Domain.Models;
using MyOrders.Domain.Persistence;
using Test.Utils.Models;

namespace Test.Utils.Repositories;

public class OrderRepositoryBuilder
{
	private static OrderRepositoryBuilder _instance;
	private readonly Mock<IOrderRepository> _orderRepository;

	private OrderRepositoryBuilder()
	{
		if (_orderRepository == null)
		{
            _orderRepository = new Mock<IOrderRepository>();
        }
    }

    public static OrderRepositoryBuilder Instance()
	{
		_instance = new OrderRepositoryBuilder();
		return _instance;
    }

	public OrderRepositoryBuilder AddAsync(Order order, CancellationToken cancellationToken)
	{
		_orderRepository.Setup(i => i.AddAsync(order, cancellationToken)).Returns(Task.CompletedTask);

		return this;
	}

	public OrderRepositoryBuilder GetOrderByIdAsync(int orderId, CancellationToken cancellationToken)
	{
        _orderRepository.Setup(i => i.GetOrderByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(OrderBuilder.Build(orderId));

        return this;
    }

	public OrderRepositoryBuilder GetOrdersAsync(CancellationToken cancellationToken)
	{
        IEnumerable<Order> orders = new List<Order> { OrderBuilder.Build(), OrderBuilder.Build() };

        _orderRepository.Setup(i => i.GetOrdersAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(orders);

        return this;
    }


    public OrderRepositoryBuilder CheckOrderIdExists(int orderId, CancellationToken cancellationToken)
	{
        _orderRepository.Setup(i => i.CheckOrderIdExists(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        return this;
    }

    public Mock<IOrderRepository> Build()
	{
		return _orderRepository;
	}
}