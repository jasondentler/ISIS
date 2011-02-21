using System;
using System.Data;

namespace ISIS
{
    public class UnexpectedNumberOfRowsAffectedException : Exception 
    {

        public UnexpectedNumberOfRowsAffectedException(IDbCommand command,
            long rowsAffected)
        {
            
        }

    }
}
