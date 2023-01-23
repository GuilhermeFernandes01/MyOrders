using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Outputs.Orders;

namespace MyOrders.Application.UseCases.Orders.Create;

public interface ICreateOrderUseCase : IUseCase<CreateOrderDto, CreateOrderResponse>
{
}