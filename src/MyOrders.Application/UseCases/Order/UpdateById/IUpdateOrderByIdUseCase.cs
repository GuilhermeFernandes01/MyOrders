using MyOrders.Application.Orders.Inputs;

namespace MyOrders.Application.UseCases.Order.UpdateById
{
	public interface IUpdateOrderByIdUseCase
	{
        Task Execute(UpdateOrderStatusByIdRequest updateOrderStatusByIdRequest, CancellationToken cancellationToken);
    }
}