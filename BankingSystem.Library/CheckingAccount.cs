namespace BankingSystem.Library
{
    public class CheckingAccount : Account, ITax
    {
        public CheckingAccount(
            int agency,
            Client client) : base(agency, client)
        {
        }

        public double CalcTax()
        {
            throw new System.NotImplementedException();
        }

        public override void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new BankingException("Saldo insuficiente.");
            double tax = 1.05;
            Balance -= amount * tax;
        }
    }
}
