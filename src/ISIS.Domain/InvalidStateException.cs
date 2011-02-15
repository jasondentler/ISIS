using System;

namespace ISIS
{
    public class InvalidStateException : ApplicationException
    {
        public InvalidStateException(string message)
            : base(message)
        {
        }
    }
}