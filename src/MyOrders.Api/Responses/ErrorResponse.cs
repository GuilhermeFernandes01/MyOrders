namespace MyOrders.Api.Responses;

public class ErrorResponse
{
	public string Message { get; }
	public List<string> ValidationsErrorMessage { get; }

	public ErrorResponse(string message)
	{
		Message = message;
	}

	public ErrorResponse(string message, List<string> validationsErrorMessages)
	{
		Message = message;
        ValidationsErrorMessage = validationsErrorMessages;
	}
}