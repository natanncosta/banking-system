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

        public virtual double GetBalanceWithYields()
        {
            return Balance;
        }

        public virtual void Deposit(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("Valor de depósito não pode ser negativo");
            if (amount == 0)
                throw new ArgumentException("Valor de depósito deve ser maior que zero");
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
            return $"Agência: {Agency}\nNúmero: {Number}\nCorrentista: {Customer.ToString()}";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Agency, Number, Customer, Balance);
        }
    }
}
