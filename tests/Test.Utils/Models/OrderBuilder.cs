using Bogus;
using Bogus.DataSets;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Domain.Models;

namespace Test.Utils.Models;

public static class OrderBuilder
{
	public static Order? Build(
        int? orderId = default,
        string? productName = default,
        int? quantity = default,
        bool? paid = default,
        bool? shipped = default
        )
	{
        orderId ??= new Faker().Random.Int(1, 10000);
        productName ??= new Faker().Commerce.ProductName();
        quantity ??= new Faker().Random.Int(1, 100);
        paid ??= false;
        shipped ??= false;

        return new Order
        {
            OrderId = orderId.Value,
            ProductName = productName,
            Quantity = quantity.Value,
            Paid = paid.Value,
            Shipped = shipped.Value
        };
    }
}

