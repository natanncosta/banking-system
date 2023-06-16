using System;
using BankingSystem.Library;
using BankingSystem.Library.Exceptions;

namespace BankingSystem.ConsoleApp
{
    public static class AccountConsole
    {
        private static AccountCRUD _crud = new();
        private static CustomerCRUD _customerCrud = new();

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1 - Adicionar conta");
            Console.WriteLine("2 - Editar conta");
            Console.WriteLine("3 - Listar todas as contas");
            Console.WriteLine("4 - Consultar conta");
            Console.WriteLine("5 - Excluir conta");
            Console.WriteLine("6 - Voltar");
            ChooseOption(Program.ReadOption());
        }

        static void ChooseOption(string option)
        {
            Console.Clear();
            switch (option)
            {
                case "1":
                    Create();
                    break;
                case "2":
                    Edit();
                    break;
                case "3":
                    ShowAll();
                    break;
                case "4":
                    ShowAccount();
                    break;
                case "5":
                    Delete();
                    break;
                case "6":
                    Program.MainMenu();
                    break;
                default:
                    Menu();
                    break;
            }
            BackToMenu();
        }

        private static void Create()
        {
            try
            {
                string menuOption = "4";
                string option = GetAccountType();
                if (option == menuOption)
                    Menu();

                Customer customer = CustomerConsole.GetCustomer();
                if (customer == null)
                {
                    BackToMenu();
                }

                Console.Write("Digite o número da agencia: ");
                int agency = Convert.ToInt32(Console.ReadLine());

                int accountNumber = Create(option, agency, customer);
                Console.WriteLine($"Número da conta: {accountNumber}");

                HandleSuccess("Conta criada com sucesso!");
            }
            catch (FormatException)
            {
                HandleWarning("Agência precisa ser um número!");
            }
            catch (ArgumentException e)
            {
                HandleError(e.Message);
            }
        }

        static string GetAccountType()
        {
            Console.WriteLine("Escolha o dipo de conta que deseja abrir");
            Console.WriteLine("1 - Conta corrente");
            Console.WriteLine("2 - Conta investimento");
            Console.WriteLine("3 - Conta poupanca");
            Console.WriteLine("4 - Voltar");
            return Program.ReadOption();
        }


        private static void Edit()
        {
            Account account = GetAccount();

            Console.Write("Digite o número da nova agencia: ");
            account.Agency = Convert.ToInt32(Console.ReadLine());
            Console.Write("Digite o CPF do novo cliente");
            account.Customer = CustomerConsole.GetCustomer();

            _crud.Update(account);

            HandleSuccess("Conta atualizada.");
        }

        private static void ShowAll()
        {
            var accounts = _crud.GetAll();

            int size = 12;
            Console.WriteLine(
                    $"{"Agência".PadRight(size)}{"| Número".PadRight(size)}{"| Correntista".PadRight(size)} | Saldo");

            foreach (var account in accounts)
            {
                var c = account.Value;
                string agency = c.Agency.ToString();
                string number = c.Number.ToString();
                string name = c.Customer.Name;
                string balance = c.GetBalanceWithYields().ToString("0.00");

                Console.Write(agency.PadRight(size));
                Console.Write(number.PadRight(size));
                Console.Write(name.PadRight(size));
                Console.Write($"R$ {balance.PadRight(size)}");
            }
        }


        private static void ShowAccount()
        {
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
                AccountActionsMenu(account);
        }

        private static void DoWithdraw(Account account)
        {
            Console.Write("Digite o valor do saque: R$ ");
            try
            {
                double amount = Convert.ToDouble(Console.ReadLine());
                account.Withdraw(amount);
                HandleSuccess("Saque realizado com sucesso.");
            }
            catch (FundsInsufficientException e)
            {
                HandleError(e.Message);

                Console.Write("\nPressione qualquer tecla...");
                Console.ReadKey();
                AccountActionsMenu(account);
            }
            catch (FormatException e)
            {
                HandleWarning(e.Message);
            }
        }

        private static void DoDeposit(Account account)
        {
            Console.Write($"Digite o valor que gostaria de depositar: R$ ");
            try
            {
                double amount = Convert.ToDouble(Console.ReadLine());
                account.Deposit(amount);
                HandleSuccess("Deposito realizado com sucesso.");
            }
            catch (ArgumentException e)
            {
                HandleError(e.Message);
            }
            catch (FormatException e)
            {
                HandleWarning(e.Message);
            }
        }

        private static void ShowBalance(Account account)
        {
            Console.Write($"\nSaldo disponível: ");
            HandleSuccess($"R$ {account.GetBalanceWithYields().ToString("0.00")}");
        }

        private static void Delete()
        {
            _crud.Delete(GetAccount());
            HandleSuccess("Conta excluida com sucesso.");
        }

        private static Account GetAccount()
        {
            int number = 0;
            try
            {
                number = GetAccountNumber();
            }
            catch (FormatException)
            {
                HandleWarning("Digite apenas números!!");
            }
            return _crud.GetAccount(number);
        }

        private static int GetAccountNumber()
        {
            Console.Write("Digite o número da conta: ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static void TotalMoney()
        {
            double totalMoney = CalcTotalMoney();

            HandleSuccess($"Montante em caixa: R$ {totalMoney.ToString("0.00")}");

            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Program.MainMenu();
        }

        static double CalcTotalMoney()
        {
            var accounts = _crud.GetAll();
            double money = 0;
            foreach (var account in accounts)
                money += account.Value.GetBalanceWithYields();
            return money;
        }

        public static void TotalTaxs()
        {
            double totalTax = CalcTotalTaxs();

            HandleSuccess($"Total de tributos: R$ {totalTax.ToString("0.00")}");

            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Program.MainMenu();
        }

        static double CalcTotalTaxs()
        {
            var accounts = _crud.GetAll();
            double totalTax = 0;
            foreach (var account in accounts)
            {
                Account c = account.Value;
                if (c is InvestmentAccount || c is CheckingAccount)
                    totalTax += ((ITax)c).CalcTax();
            }
            return totalTax;
        }

        static void HandleError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void HandleWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void HandleSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void BackToMenu()
        {
            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Menu();
        }

        static int Create(string accountType, int agency, Customer customer)
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
    }
}
