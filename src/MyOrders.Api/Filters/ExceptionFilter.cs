using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyOrders.Api.Responses;
using MyOrders.Domain.Exceptions;

namespace MyOrders.Api.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError("ERROR: {error}", context);

            switch (context?.Exception)
            {
                case MyOrdersException:
                    HandleMyOrdersExceptions(context);
                    break;
                default:
                    HandleUnknownError(context);
                    break;
            }
        }

        private static void HandleMyOrdersExceptions(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case ValidationErrorsException:
                    HandleValidationErrorsException(context);
                    break;
                case NotFoundException:
                    HandleNotFoundException(context);
                    break;
                default:
                    break;
            }
        }

        private static void HandleValidationErrorsException(ExceptionContext context)
        {
            var validationErrorException = context.Exception as ValidationErrorsException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new ObjectResult(new ErrorResponse("Malformed payload", validationErrorException.ValidationsErrorMessages));
        }

        private static void HandleNotFoundException(ExceptionContext context)
        {
            var validationErrorException = context.Exception as NotFoundException;

            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            context.Result = new ObjectResult(new ErrorResponse(validationErrorException.Message));
        }

        private static void HandleUnknownError(ExceptionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Result = new ObjectResult(new ErrorResponse("Internal Server Error"));
        }
    }
}

