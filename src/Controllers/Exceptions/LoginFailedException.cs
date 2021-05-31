using System;

namespace oauth2testbed.Controllers.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string message) : base(message) { }
    }
}
