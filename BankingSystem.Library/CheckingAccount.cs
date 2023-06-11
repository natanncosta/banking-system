using BankingSystem.Library.Exceptions;

namespace BankingSystem.Library
{
    public class CheckingAccount : Account, ITax
    {
        public CheckingAccount(
            int agency,
            Customer customer) : base(agency, customer)
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
            double tax = 1.05;
            Balance -= amount * tax;
        }
    }
}
