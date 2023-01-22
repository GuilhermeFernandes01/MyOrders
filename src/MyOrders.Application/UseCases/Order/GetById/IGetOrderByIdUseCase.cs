using MyOrders.Application.Orders.Inputs;
using MyOrders.Application.Orders.Outputs;

namespace MyOrders.Application.UseCases.Order.GetById
{
	public interface IGetOrderByIdUseCase
	{
        Task<GetOrderByIdResponse> Execute(GetOrderByIdRequest getOrderByIdRequest, CancellationToken cancellationToken);
    }
}