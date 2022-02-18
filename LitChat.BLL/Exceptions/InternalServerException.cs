using System;
using System.Globalization;

namespace LitChat.BLL.Exceptions
{
    public class InternalServerException : Exception
    {
        public InternalServerException() : base() { }

        public InternalServerException(string message) : base(message) { }

        public InternalServerException(string message, params object[] args) :
            base(string.Format(CultureInfo.CurrentCulture, message, args))
        { }

    }
}
