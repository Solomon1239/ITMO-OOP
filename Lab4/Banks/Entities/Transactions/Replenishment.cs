using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.Banks.Transactions
{
    public class Replenishment : Transaction
    {
        public Replenishment(decimal value, IAccount account)
            : base(value, account)
        {
        }

        public override void Execute()
        {
            Account.Replenishment(Value);
        }

        public override void Cancel()
        {
            if (!CanselIsAvailable) throw new AccountException("Transaction already cancelled");

            Account.Withdrawal(Value);
            CanselIsAvailable = false;
        }
    }
}