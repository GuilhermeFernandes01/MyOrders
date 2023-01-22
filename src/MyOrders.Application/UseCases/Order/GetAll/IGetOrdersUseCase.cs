using MyOrders.Application.Orders.Outputs;

namespace MyOrders.Application.UseCases.Order.GetAll
{
	public interface IGetOrdersUseCase
	{
		Task<GetAllOrdersResponse> Execute(CancellationToken cancellationToken);
    }
}