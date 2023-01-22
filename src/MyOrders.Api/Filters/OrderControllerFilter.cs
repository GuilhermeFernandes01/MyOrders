using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyOrders.Application.Validators.Orders;
using MyOrders.Application.Orders.Inputs;
using MyOrders.Domain.Exceptions;
using Microsoft.Extensions.Logging;
using MyOrders.Api.Controllers;

namespace MyOrders.Api.Filters;

public class OrderControllerFilter : ActionFilterAttribute
{
    public OrderControllerFilter()
    {
    }

    public override void OnActionExecuting(ActionExecutingContext context)
	{
        var request = context.ActionArguments
            .FirstOrDefault(x => x.Value is CreateOrderRequest).Value as CreateOrderRequest;
        var validator = new CreateOrderValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ValidationErrorsException("Malformed payload", errorMessages);
        }
    }
}