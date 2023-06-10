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
            return Balance * 0.10;
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
