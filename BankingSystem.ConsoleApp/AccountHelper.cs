using System;
using System.Collections.Generic;
using BankingSystem.Library;

namespace BankingSystem.ConsoleApp
{
    public class AccountHelper
    {
        private AccountCRUD _crud = new AccountCRUD();

        public int Create(string accountType, int agency, Customer customer)
        {
            Account account = null;
            switch (accountType)
            {
                case "1":
                    account = new CheckingAccount(agency, customer);
                    break;
                case "2":
                    account = new InvestmentAccount(agency, customer);
                    break;
                case "3":
                    account = new SavingsAccount(agency, customer);
                    break;
                default:
                    throw new ArgumentException("Opcao invalida");
            }

            _crud.Add(account);
            return account.Number;
        }

        public string GetAccountType()
        {
            Console.WriteLine("Escolha o dipo de conta que deseja abrir");
            Console.WriteLine("1 - Conta corrente");
            Console.WriteLine("2 - Conta investimento");
            Console.WriteLine("3 - Conta poupanca");
            Console.WriteLine("4 - Voltar");
            return Program.ReadOption();
        }

        public double CalcTotalMoney()
        {
            var accounts = GetAll();
            double money = 0;
            foreach (var account in accounts)
                money += account.GetBalanceWithYields();
            return money;
        }

        public List<Account> GetAll()
        {
            return _crud.GetAll();
        }

        public void Update(Account sourceAccount, Account updatedAccount)
        {
            _crud.Update(sourceAccount, updatedAccount);
        }

        public void Delete(Account account)
        {
            _crud.Delete(account);
        }

        public Account Get(int number)
        {
            return _crud.GetAccount(number);
        }

        public double CalcTotalTaxs()
        {
            var accounts = GetAll();
            double totalTax = 0;
            foreach (var account in accounts)
                if (account is InvestmentAccount || account is CheckingAccount)
                    totalTax += ((ITax)account).CalcTax();
            return totalTax;
        }

        public Customer GetCustomer()
        {
            return CustomerConsole.GetCustomer();
        }

        public void HandleError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void HandleWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void HandleSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
