using Bogus;
using MyOrders.Application.DTOs.Orders;

namespace Test.Utils.DTOs.Orders;

public class GetOrderByIdDtoBuilder
{
    public static GetOrderByIdDto Build()
    {
        var orderId = new Faker().Random.Int(1, 10000);

        return new GetOrderByIdDto(orderId);
    }
}