using System;

namespace BankingSystem.Library
{
    public abstract class Account
    {
        static int amountOfAccounts = 1;
        public int Agency { get; set; }
        public int Number { get; set; }
        public Client Client { get; set; }
        public double Balance { get; protected set; }

        public Account(int agency, Client client)
        {
            Agency = agency;
            Client = client;
            Number = AmountOfAccounts();
            Balance = 0;
            amountOfAccounts++;
        }

        public abstract void Withdraw(double amount);

        public virtual void Deposit(double amount)
        {
            Balance += amount;
        }

        public int AmountOfAccounts()
        {
            return amountOfAccounts;
        }

        public override bool Equals(object account)
        {
            Account act;
            if (account is Account)
            {
                act = (Account)account;
                return act.Number == Number;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Agency, Number, Client, Balance);
        }
    }
}
