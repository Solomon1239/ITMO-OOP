using System;

namespace Banks.Tools
{
    public class AccountException : Exception
    {
        public AccountException(string message)
            : base(message)
        {
        }
    }
}