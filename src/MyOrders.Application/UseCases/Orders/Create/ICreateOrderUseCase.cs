using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Orders.Outputs;

namespace MyOrders.Application.UseCases.Orders.Create;

public interface ICreateOrderUseCase : IUseCase<CreateOrderDto, CreateOrderResponse>
{
}