namespace MyOrders.Api.Responses
{
	public class ErrorResponse
	{
		public List<string> Messages { get; }

		public ErrorResponse(string message)
		{
			Messages = new List<string>
			{
				message
			};
		}

		public ErrorResponse(List<string> messages)
		{
			Messages = messages;
		}
	}
}

