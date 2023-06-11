using BankingSystem.Library.Exceptions;

namespace BankingSystem.Library
{
    public class SavingsAccount : Account
    {
        public const double YIELDS_PERCENTAGE = 0.005;

        public SavingsAccount(
            int agency,
            Customer client) : base(agency, client)
        {
        }

        public override double GetBalanceWithYields()
        {
            return Balance + (Balance * YIELDS_PERCENTAGE);
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
