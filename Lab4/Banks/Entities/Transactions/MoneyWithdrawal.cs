using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.Banks.Transactions
{
    public class MoneyWithdrawal : Transaction
    {
        public MoneyWithdrawal(decimal value, IAccount account)
            : base(value, account)
        {
        }

        public override void Execute()
        {
            Account.Withdrawal(Value);
        }

        public override void Cancel()
        {
            if (!CanselIsAvailable) throw new AccountException("Transaction already cancelled");

            Account.Replenishment(Value);
            CanselIsAvailable = false;
        }
    }
}