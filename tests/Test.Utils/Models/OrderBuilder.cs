using Bogus;
using Bogus.DataSets;
using MyOrders.Application.DTOs.Orders;
using MyOrders.Domain.Models;

namespace Test.Utils.Models;

public static class OrderBuilder
{
	public static Order Build()
	{
        var orderId = new Faker().Random.Int(1, 10000);
        var productName = new Faker().Commerce.ProductName();
        var quantity = new Faker().Random.Int(1, 100);
        var paid = false;
        var shipped = false;

        return new Order
        {
            OrderId = orderId,
            ProductName = productName,
            Quantity = quantity,
            Paid = paid,
            Shipped = shipped
        };
    }
}

