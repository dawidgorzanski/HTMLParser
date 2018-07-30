using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Exceptions
{
    public class InvalidSyntaxException: Exception
    {
        public InvalidSyntaxException()
        {

        }

        public InvalidSyntaxException(string message): base(message)
        {
            
        }

        public InvalidSyntaxException(string message, Exception innerException): base(message, innerException)
        {

        }
    }
}
