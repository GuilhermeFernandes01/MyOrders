using MyOrders.Application.DTOs.Orders;
using MyOrders.Application.Outputs.Orders;

namespace MyOrders.Application.UseCases.Orders.UpdateById;

public interface IUpdateOrderByIdUseCase : IUseCase<UpdateOrderStatusByIdDTO, UpdateOrderByIdResponse?>
{
}