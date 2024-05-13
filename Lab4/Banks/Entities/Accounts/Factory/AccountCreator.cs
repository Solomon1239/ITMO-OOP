using System;

namespace Banks.Entities.Accounts
{
    public abstract class AccountCreator
    {
        public abstract IAccount Create();
    }
}