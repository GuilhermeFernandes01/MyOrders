using System.Runtime.Serialization;

namespace MyOrders.Domain.Exceptions
{
    [Serializable]
	public class ValidationErrorsException : MyOrdersException
    {
		public List<string>? ValidationsErrorMessages { get; }

        public ValidationErrorsException()
        {
        }

        public ValidationErrorsException(string message, List<string> errorMessages) : base(message)
		{
            ValidationsErrorMessages = errorMessages;
		}


        public ValidationErrorsException(string message) : base(message)
        {
        }

        public ValidationErrorsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValidationErrorsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}