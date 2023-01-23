using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Outputs.Orders;

namespace MyOrders.Application.UseCases.Orders.GetById;

public interface IGetOrderByIdUseCase : IUseCase<GetOrderByIdDto, GetOrderByIdResponse>
{
}