using Magic8Ball.Application.Errors;
using System;
using System.Collections.Generic;

namespace Magic8Ball.Application.Exceptions
{
    public class BaseException : Exception
    {
        public int StatusCodeException { get; }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, int statusCodeException) : base(message)
        {
            StatusCodeException = statusCodeException;
        }

    }
}
