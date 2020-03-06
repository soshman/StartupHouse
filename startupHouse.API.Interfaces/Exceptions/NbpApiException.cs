using System;

namespace StartupHouse.API.Interfaces.Exceptions
{
    /// <summary>
    ///     Exception caused by problem connected with NBP Api calls.
    /// </summary>
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
