using BankingSystem.Library.Exceptions;

namespace BankingSystem.Library
{
    public class CheckingAccount : Account, ITax
    {
        public const double TAX_PERCENTAGE = 0.10;

        public CheckingAccount(
            int agency,
            Customer customer) : base(agency, customer)
        {
        }

        public double CalcTax()
        {
            return Balance * TAX_PERCENTAGE;
        }

        public override void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new FundsInsufficientException();
            double tax = 1.05;
            Balance -= amount * tax;
        }
    }
}
