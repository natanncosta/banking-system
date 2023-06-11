using BankingSystem.Library.Exceptions;

namespace BankingSystem.Library
{
    public class InvestmentAccount : Account, ITax
    {
        public InvestmentAccount(int agency, Customer client) : base(agency, client)
        {
        }

        public double CalcTax()
        {
            double taxPercentage = 0.10;
            double balanceWithYields = Balance * 1.02;
            return balanceWithYields * taxPercentage;
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
