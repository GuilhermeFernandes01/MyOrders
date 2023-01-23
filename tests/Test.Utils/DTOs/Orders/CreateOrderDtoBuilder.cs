using Bogus;
using MyOrders.Application.DTOs.Orders;

namespace Test.Utils.DTOs.Orders;

public static class CreateOrderDtoBuilder
{
    public static CreateOrderDto Build()
    {
        var productName = new Faker().Commerce.ProductName();
        var quantity = new Faker().Random.Int(1, 100);

        return new CreateOrderDto(productName, quantity);
    }
}