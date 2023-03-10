using FluentValidation;
using MyOrders.Application.Inputs.Orders;

namespace MyOrders.Application.Validators.Orders;

public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
{
	public CreateOrderValidator()
	{
        RuleFor(c => c.ProductName).NotEmpty();
        RuleFor(c => c.Quantity).GreaterThan(0);
    }
}