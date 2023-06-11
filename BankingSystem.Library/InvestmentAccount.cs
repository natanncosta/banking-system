using BankingSystem.Library.Exceptions;

namespace BankingSystem.Library
{
    public class InvestmentAccount : Account, ITax
    {
        public const double TAX_PERCENTAGE = 0.10;
        public const double YIELDS_PERCENTAGE = 0.02;

        public InvestmentAccount(int agency, Customer client) : base(agency, client)
        {
        }

        public override double GetBalanceWithYields()
        {
            return Balance + (Balance * YIELDS_PERCENTAGE);
        }

        public double CalcTax()
        {
            double balanceWithYields = Balance + (Balance * YIELDS_PERCENTAGE);
            return balanceWithYields * TAX_PERCENTAGE;
        }

        public override void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new FundsInsufficientException();
            double tax = 1.075;
            Balance -= amount * tax;
        }
    }
}
