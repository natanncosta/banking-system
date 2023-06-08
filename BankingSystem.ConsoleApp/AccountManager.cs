using System;
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
                    GetAccount(); break;
                case "5":
                    DeleteAccount(); break;
                case "6":
                    Program.MainMenu(); break;
                default:
                    Menu(); break;
            }
        }

        private static void DeleteAccount()
        {
            throw new NotImplementedException();
        }

        private static Account GetAccount()
        {
            Account account = null;
            try
            {
                Console.Write("Digite o numero da conta: ");
                int number = Convert.ToInt32(Console.ReadLine());
                account = _accountCrud.GetAccount(number);
            }
            catch (ArgumentException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                GetAccount();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Digite apenas numeros!");
                Console.ResetColor();
                GetAccount();
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

            Account account = GetAccount();
            Console.Write("Digite o número da nova agencia: ");
            account.Agency = Convert.ToInt32(Console.ReadLine());
            account.Client = ClientManager.GetClient();

            _accountCrud.Update(account);

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
