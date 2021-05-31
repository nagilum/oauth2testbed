using System;

namespace oauth2testbed.Controllers.Exceptions
{
    public class ClientNotFoundException : Exception
    {
        public ClientNotFoundException(string message) : base(message) { }
    }
}