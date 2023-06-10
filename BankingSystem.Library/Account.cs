using System;

namespace BankingSystem.Library
{
    public abstract class Account
    {
        static int amountOfAccounts = 1;
        public int Agency { get; set; }
        public int Number { get; set; }
        public Customer Customer { get; set; }
        public double Balance { get; protected set; }

        public Account(int agency, Customer customer)
        {
            Agency = agency;
            Customer = customer;
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

        public override bool Equals(object obj)
        {
            if (obj is not Account) return false;

            Account account = (Account)obj;
            return Number == account.Number;
        }

        public override string ToString()
        {
            return "Agência: " + Agency
                + "Número: " + Number
                + "Correntista: " + Customer.ToString();
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Agency, Number, Customer, Balance);
        }
    }
}
