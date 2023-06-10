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
            throw new System.NotImplementedException();
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
