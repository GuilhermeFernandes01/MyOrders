using System;
namespace MyOrders.Domain.Exceptions;

public class OrderUpdateFailedException : MyOrdersException
{
	public OrderUpdateFailedException() : base("ERROR: Order didn't update")
	{
	}

    public OrderUpdateFailedException(string message) : base(message)
    {
    }

    public OrderUpdateFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

