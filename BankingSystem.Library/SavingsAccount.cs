using BankingSystem.Library.Exceptions;

namespace BankingSystem.Library
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(
            int agency,
            Customer client) : base(agency, client)
        {
        }

        public override void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new FundsInsufficientException();
            double tax = 1.01;
            Balance -= amount * tax;
        }
    }
}
