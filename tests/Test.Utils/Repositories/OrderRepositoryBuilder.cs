using Moq;
using MyOrders.Domain.Models;
using MyOrders.Domain.Persistence;

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

    public IOrderRepository Build()
	{
		return _orderRepository.Object;
	}
}