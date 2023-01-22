using MyOrders.Application.Orders.Inputs;
using MyOrders.Application.Orders.Outputs;

namespace MyOrders.Application.UseCases.Order.Create
{
    public interface ICreateOrderUseCase
    {
        Task<CreateOrderResponse> Execute(CreateOrderRequest createOrderRequest, CancellationToken cancellationToken);
    }
}