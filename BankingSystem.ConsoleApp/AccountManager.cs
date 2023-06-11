using System;
using System.Threading;
using System.Collections.Generic;
using BankingSystem.Library;
using BankingSystem.Library.Exceptions;

namespace BankingSystem.ConsoleApp
{
    public static class AccountManager
    {
        static AccountCRUD _accountCrud = new();

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1 - Adicionar conta");
            Console.WriteLine("2 - Editar conta");
            Console.WriteLine("3 - Listar todas as contas");
            Console.WriteLine("4 - Consultar conta");
            Console.WriteLine("5 - Excluir conta");
            Console.WriteLine("6 - Voltar");
            CallOption(Program.ReadOption());
        }

        static void CallOption(string option)
        {
            switch (option)
            {
                case "1":
                    CreateAccount(); break;
                case "2":
                    EditAccount(); break;
                case "3":
                    ShowAllAccounts(); break;
                case "4":
                    ShowAccount(); break;
                case "5":
                    DeleteAccount(); break;
                case "6":
                    Program.MainMenu(); break;
                default:
                    Menu(); break;
            }
        }

        private static void ShowAccount()
        {
            Console.Clear();
            Account account = GetAccount();
            Console.WriteLine(account.ToString());
            Console.WriteLine();
            AccountActionsMenu(account);
        }

        private static void AccountActionsMenu(Account account)
        {
            Console.WriteLine("1 - Sacar");
            Console.WriteLine("2 - Depositar");
            Console.WriteLine("3 - Ver Saldo");
            Console.Write("=> ");
            string option = Console.ReadLine();
            if (option == "1")
                DoWithdraw(account);
            else if (option == "2")
                DoDeposit(account);
            else if (option == "3")
                ShowBalance(account);
            else
            {
                AccountActionsMenu(account);
            }
        }

        private static void ShowBalance(Account account)
        {
            Console.Write($"\nSaldo disponível: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("R$ " + account.Balance.ToString("0.00"));
            Console.ResetColor();

            BackToMenu();
        }

        private static void DoWithdraw(Account account)
        {
            Console.Clear();

            try
            {
                Console.Write("Digite o valor do saque: R$ ");
                double amount = Convert.ToDouble(Console.ReadLine());
                account.Withdraw(amount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Saque realizado com sucesso.");
                Console.ResetColor();

                BackToMenu();
            }
            catch (FundsInsufficientException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                Thread.Sleep(2000);
                AccountActionsMenu(account);
            }
        }

        private static void DoDeposit(Account account)
        {
            Console.Clear();

            Console.Write($"Digite o valor que gostaria de depositar: R$ ");
            double amount = Convert.ToDouble(Console.ReadLine());
            account.Deposit(amount);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Deposito realizado com sucesso.");
            Console.ResetColor();

            BackToMenu();
        }

        private static void DeleteAccount()
        {
            Console.Clear();

            Account account = GetAccount();
            _accountCrud.Delete(account);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conta excluida com sucesso.");
            Console.ResetColor();

            BackToMenu();
        }

        private static Account GetAccount()
        {
            Console.Write("Digite o numero da conta: ");
            try
            {
                int number = Convert.ToInt32(Console.ReadLine());

                return _accountCrud.GetAccount(number);
            }
            catch (AccountNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                BackToMenu();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Digite apenas números!!");
                Console.ResetColor();
                BackToMenu();
            }
            return null;
        }

        private static void ShowAllAccounts()
        {
            List<Account> accounts = _accountCrud.GetAll();
            Console.Clear();
            int p = 12; // pad size
            Console.WriteLine("Agência".PadRight(p) + "| Número".PadRight(p) + "| Correntista".PadRight(p) + "| Saldo");
            foreach (var account in accounts)
            {
                Console.Write($"{account.Agency}".PadRight(p));
                Console.Write($"{account.Number}".PadRight(p));
                Console.Write($"{account.Customer.Name}".PadRight(p));
                Console.Write($"R$ {account.Balance.ToString("0.00")}".PadRight(p));
                Console.WriteLine();
            }
            BackToMenu();
        }

        private static void EditAccount()
        {
            Console.Clear();

            Account oldAccount = GetAccount();
            Account updatedAccount = oldAccount;

            Console.Write("Digite o número da nova agencia: ");
            updatedAccount.Agency = Convert.ToInt32(Console.ReadLine());
            Console.Write("Digite o CPF do novo cliente");
            updatedAccount.Customer = CustomerManager.GetCustomer();

            _accountCrud.Update(oldAccount, updatedAccount);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conta atualizada.");
            Console.ResetColor();
            BackToMenu();
        }

        private static void CreateAccount()
        {
            string type = AccountTypeMenu();
            Console.Clear();

            Customer customer = CustomerManager.GetCustomer();
            Console.Write("Digite o número da agencia: ");
            int agency = 0;
            try
            {
                agency = Convert.ToInt32(Console.ReadLine());

                Account account = null;
                if (type == "1")
                    account = new CheckingAccount(agency, customer);
                else if (type == "2")
                    account = new InvestmentAccount(agency, customer);
                else if (type == "3")
                    account = new SavingsAccount(agency, customer);

                Console.WriteLine("Número da conta: " + account.Number);

                _accountCrud.Add(account);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Agência precisa ser um número!");
                Console.ResetColor();
            }

            BackToMenu();
        }

        private static string AccountTypeMenu()
        {
            Console.Clear();
            Console.WriteLine("Escolha o dipo de conta que deseja abrir");
            Console.WriteLine("1 - Conta corrente");
            Console.WriteLine("2 - Conta investimento");
            Console.WriteLine("3 - Conta poupanca");
            Console.WriteLine("4 - Voltar");
            string option = Program.ReadOption();

            if (option == "4")
                Menu();
            if (option != "1" && option != "2" && option != "3")
                AccountTypeMenu();
            return option;
        }

        public static void TotalMoney()
        {
            Console.Clear();

            var accounts = _accountCrud.GetAll();

            double moneyInCash = 0;
            foreach (var account in accounts)
            {
                double percentageOfYield = 1;

                if (account is InvestmentAccount)
                    percentageOfYield += 0.02;
                else if (account is SavingsAccount)
                    percentageOfYield += 0.005;

                moneyInCash += account.Balance * percentageOfYield;
            }

            Console.Write("Montante em caixa: ");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("R$ " + moneyInCash.ToString("0.00"));
            Console.ResetColor();

            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Program.MainMenu();
        }

        public static void TotalTaxs()
        {
            Console.Clear();

            var accounts = _accountCrud.GetAll();

            double totalTax = 0;
            foreach (var account in accounts)
                if (account is InvestmentAccount)
                    totalTax += ((InvestmentAccount)account).CalcTax();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Total de tributos: R$ " + totalTax.ToString("0.00"));
            Console.ResetColor();

            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Program.MainMenu();
        }



        static void BackToMenu()
        {
            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            AccountManager.Menu();
        }
    }
}
