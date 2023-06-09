using System;
using System.Threading;
using System.Collections.Generic;
using BankingSystem.Library;

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

            BackToAccountMenu();
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

                BackToAccountMenu();
            }
            catch (BankingException e)
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

            BackToAccountMenu();
        }

        private static void DeleteAccount()
        {
            Console.Clear();

            Account account = GetAccount();
            _accountCrud.Delete(account);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conta excluida com sucesso.");
            Console.ResetColor();

            BackToAccountMenu();
        }

        private static Account GetAccount()
        {
            Console.Write("Digite o numero da conta: ");
            int number = Convert.ToInt32(Console.ReadLine());

            Account account = _accountCrud.GetAccount(number);
            if (account is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Conta não encontrada.");
                Console.ResetColor();
                BackToAccountMenu();
            }
            return account;
        }

        private static void ShowAllAccounts()
        {
            List<Account> accounts = _accountCrud.GetAll();
            if (accounts is not null)
            {
                int p = 10; // pad size
                Console.WriteLine("Agência".PadRight(p) + "| Número".PadRight(p) + "| Correntista".PadRight(p) + "| Saldo");
                foreach (var account in accounts)
                {
                    Console.Write($"{account.Agency}".PadRight(p));
                    Console.Write($"{account.Number}".PadRight(p));
                    Console.Write($"{account.Client}".PadRight(p));
                    Console.Write($"{account.Balance}".PadRight(p));
                }
            }
            BackToAccountMenu();
        }

        private static void EditAccount()
        {
            Console.Clear();

            Account oldAccount = GetAccount();
            Account updatedAccount = oldAccount;

            Console.Write("Digite o número da nova agencia: ");
            updatedAccount.Agency = Convert.ToInt32(Console.ReadLine());
            Console.Write("Digite o CPF do novo cliente");
            updatedAccount.Client = ClientManager.GetClient();

            _accountCrud.Update(oldAccount, updatedAccount);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conta atualizada.");
            Console.ResetColor();
            BackToAccountMenu();
        }

        private static void CreateAccount()
        {
            string type = AccountTypeMenu();
            Console.Clear();

            Client client = ClientManager.GetClient();
            Console.Write("Digite a agencia: ");
            int agency = Convert.ToInt32(Console.ReadLine());

            Account account = null;
            if (type == "1")
                account = new CheckingAccount(agency, client);
            else if (type == "2")
                account = new InvestmentAccount(agency, client);
            else if (type == "3")
                account = new SavingsAccount(agency, client);

            _accountCrud.Add(account);

            BackToAccountMenu();
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

        static void BackToAccountMenu()
        {
            Console.WriteLine("\nDite...");
            Console.ReadKey();
            AccountManager.Menu();
        }
    }
}
