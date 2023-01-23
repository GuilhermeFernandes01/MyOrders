using System.Runtime.Serialization;

namespace MyOrders.Domain.Exceptions;

[Serializable]
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

    protected OrderUpdateFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}