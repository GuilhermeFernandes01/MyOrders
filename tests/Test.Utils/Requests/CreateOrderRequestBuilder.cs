using Bogus;
using Bogus.DataSets;
using MyOrders.Application.Orders.Inputs;

namespace Test.Utils.Requests;

public static class CreateOrderRequestBuilder
{
	public static CreateOrderRequest Build()
	{
		return new Faker<CreateOrderRequest>()
		.RuleFor(r => r.ProductName, f => f.Commerce.ProductName())
		.RuleFor(r => r.Quantity, f => f.Random.Int(1, 100));
    }
}