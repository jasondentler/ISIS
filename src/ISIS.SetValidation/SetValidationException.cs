using System;

namespace ISIS
{
    public class SetValidationException : Exception
    {

        public SetValidationException(string message)
            : base (message)
        {
        }

        public SetValidationException(string format, params object[] args)
            : base(string.Format(format, args))
        {
        }

    }
}
