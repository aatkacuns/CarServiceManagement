using System;
using System.Runtime.Serialization;

namespace CarServiceManagement.Exceptions
{
    public class ValueNotFoundException : Exception
    {
        public ValueNotFoundException()
        {
        }

        public ValueNotFoundException(string message) : base(message)
        {
        }

        public ValueNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueNotFoundException(SerializationInfo serializationInfo,
            StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }
    }
}