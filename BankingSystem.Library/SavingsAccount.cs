namespace BankingSystem.Library
{
    public class SavingsAccount : Account
    {
        public SavingsAccount(
            int agency,
            Client client) : base(agency, client)
        {
        }


        public override void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new BankingException("Saldo insuficiente.");
            double tax = 1.01;
            Balance -= amount * tax;
        }
    }
}
