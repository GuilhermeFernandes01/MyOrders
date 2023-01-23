using System.Runtime.Serialization;

namespace MyOrders.Domain.Exceptions;

[Serializable]
public class MyOrdersException : SystemException
	{
    public MyOrdersException()
    {
    }

    public MyOrdersException(string message) : base(message)
		{
		}

    public MyOrdersException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected MyOrdersException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}