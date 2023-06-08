namespace BankingSystem.Library
{
    public class InvestmentAccount : Account, ITax
    {
        public InvestmentAccount(int agency, Client client) : base(agency, client)
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
            double tax = 1.075;
            Balance -= amount * tax;
        }
    }
}
