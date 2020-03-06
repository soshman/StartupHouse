using System;

namespace StartupHouse.API.Interfaces.Exceptions
{
    public class NbpApiException : Exception
    {
        public NbpApiException(string message) : base(message)
        {
        }

        public NbpApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
