using System;

namespace ISIS
{
    public class MissingCommandValidatorException : Exception
    {

        public MissingCommandValidatorException(Type commandType)
            : base(string.Format("No validator for {0}", commandType))
        {
        }

    }
}
