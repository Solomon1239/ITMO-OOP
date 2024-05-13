using Banks.Entities.Accounts;
using Banks.Tools;

namespace Banks.Entities.Banks.Transactions
{
    public class MoneyTransfer : Transaction
    {
        public MoneyTransfer(decimal value, IAccount account, IAccount receiverAccount)
            : base(value, account)
        {
            ReceiverAccount = receiverAccount;
        }

        public IAccount ReceiverAccount { get; }

        public override void Execute()
        {
            Account.Withdrawal(Value);
            ReceiverAccount.Replenishment(Value);
        }

        public override void Cancel()
        {
            if (!CanselIsAvailable) throw new AccountException("Transaction already cancelled");

            ReceiverAccount.Withdrawal(Value);
            Account.Replenishment(Value);

            CanselIsAvailable = false;
        }
    }
}