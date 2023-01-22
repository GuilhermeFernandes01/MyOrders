using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Orders.Outputs;

namespace MyOrders.Application.UseCases.Orders.GetById;

public interface IGetOrderByIdUseCase : IUseCase<GetOrderByIdDTO, GetOrderByIdResponse>
{
}