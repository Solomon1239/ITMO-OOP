using System;

namespace Banks.Tools
{
    public class ClientException : Exception
    {
        public ClientException(string message)
            : base(message)
        {
        }
    }
}