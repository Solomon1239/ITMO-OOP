using System;

namespace Banks.Entities.Accounts
{
    public class DebitAccountCreator : AccountCreator
    {
        private decimal _percent;
        private Guid _id;
        public DebitAccountCreator(decimal percent, Guid id)
        {
            _percent = percent;
            _id = id;
        }

        public override IAccount Create()
        {
            DebitAccount account = new DebitAccount(_percent, _id);

            return account;
        }
    }
}