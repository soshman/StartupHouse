using System;
using System.Runtime.Serialization;

namespace StartupHouse.API.Services
{
    [Serializable]
    internal class NbpApiException : Exception
    {
        public NbpApiException()
        {
        }

        public NbpApiException(string message) : base(message)
        {
        }

        public NbpApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NbpApiException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}