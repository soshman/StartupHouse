using System;

namespace StartupHouse.API.Interfaces.Exceptions
{
    /// <summary>
    ///     Exception indicating that object couldn't be find.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
