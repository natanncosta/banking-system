using System;
using BankingSystem.Library;
using BankingSystem.Library.Exceptions;

namespace BankingSystem.ConsoleApp
{
    public static class AccountConsole
    {
        private static AccountHelper _helper = new AccountHelper();

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
        }

        private static void Create()
        {
            Console.Clear();

            try
            {
                string menuOption = "4";
                string option = _helper.GetAccountType();
                if (option == menuOption)
                    Menu();

                Customer customer = _helper.GetCustomer();

                Console.Write("Digite o número da agencia: ");
                int agency = Convert.ToInt32(Console.ReadLine());

                int accountNumber = _helper.Create(option, agency, customer);
                Console.WriteLine($"Número da conta: {accountNumber}");

                _helper.HandleSuccess("Conta criada com sucesso!");
            }
            catch (FormatException)
            {
                _helper.HandleWarning("Agência precisa ser um número!");
            }
            catch (ArgumentException e)
            {
                _helper.HandleError(e.Message);
            }
            BackToMenu();
        }


        private static void Edit()
        {
            Console.Clear();

            Account sourceAccount = GetAccount();
            Account updatedAccount = sourceAccount;

            Console.Write("Digite o número da nova agencia: ");
            updatedAccount.Agency = Convert.ToInt32(Console.ReadLine());
            Console.Write("Digite o CPF do novo cliente");
            updatedAccount.Customer = CustomerConsole.GetCustomer();

            _helper.Update(sourceAccount, updatedAccount);

            _helper.HandleSuccess("Conta atualizada.");
            BackToMenu();
        }

        private static void ShowAll()
        {
            Console.Clear();

            var accounts = _helper.GetAll();

            int size = 12;
            Console.WriteLine(
                    $"{"Agência".PadRight(size)}{"| Número".PadRight(size)}{"| Correntista".PadRight(size)} | Saldo");

            foreach (var account in accounts)
            {
                string agency = account.Agency.ToString();
                string number = account.Number.ToString();
                string name = account.Customer.Name;
                string balance = account.GetBalanceWithYields().ToString("0.00");

                Console.Write(agency.PadRight(size));
                Console.Write(number.PadRight(size));
                Console.Write(name.PadRight(size));
                Console.Write($"R$ {balance.PadRight(size)}");
            }

            BackToMenu();
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
                AccountActionsMenu(account);
        }

        private static void DoWithdraw(Account account)
        {
            Console.Clear();

            Console.Write("Digite o valor do saque: R$ ");
            try
            {
                double amount = Convert.ToDouble(Console.ReadLine());
                account.Withdraw(amount);
                _helper.HandleSuccess("Saque realizado com sucesso.");
            }
            catch (FundsInsufficientException e)
            {
                _helper.HandleError(e.Message);

                Console.Write("\nPressione qualquer tecla...");
                Console.ReadKey();
                AccountActionsMenu(account);
            }
            catch (FormatException e)
            {
                _helper.HandleWarning(e.Message);
            }
            finally { BackToMenu(); }
        }

        private static void DoDeposit(Account account)
        {
            Console.Clear();

            Console.Write($"Digite o valor que gostaria de depositar: R$ ");
            try
            {
                double amount = Convert.ToDouble(Console.ReadLine());
                account.Deposit(amount);
                _helper.HandleSuccess("Deposito realizado com sucesso.");
            }
            catch (ArgumentException e)
            {
                _helper.HandleError(e.Message);
            }
            catch (FormatException e)
            {
                _helper.HandleWarning(e.Message);
            }
            finally { BackToMenu(); }
        }

        private static void ShowBalance(Account account)
        {
            Console.Write($"\nSaldo disponível: ");
            _helper.HandleSuccess($"R$ {account.GetBalanceWithYields().ToString("0.00")}");
            BackToMenu();
        }

        private static void Delete()
        {
            Console.Clear();

            _helper.Delete(GetAccount());

            _helper.HandleSuccess("Conta excluida com sucesso.");
            BackToMenu();
        }

        private static Account GetAccount()
        {
            Console.Write("Digite o número da conta: ");
            try
            {
                int number = Convert.ToInt32(Console.ReadLine());
                return _helper.Get(number);
            }
            catch (AccountNotFoundException e)
            {
                _helper.HandleError(e.Message);
            }
            catch (FormatException)
            {
                _helper.HandleWarning("Digite apenas números!!");
            }
            BackToMenu();
            return null;
        }

        public static void TotalMoney()
        {
            Console.Clear();

            double totalMoney = _helper.CalcTotalMoney();

            _helper.HandleSuccess($"Montante em caixa: R$ {totalMoney.ToString("0.00")}");

            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Program.MainMenu();
        }

        public static void TotalTaxs()
        {
            Console.Clear();

            double totalTax = _helper.CalcTotalTaxs();

            _helper.HandleSuccess($"Total de tributos: R$ {totalTax.ToString("0.00")}");

            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Program.MainMenu();
        }

        static void BackToMenu()
        {
            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            Menu();
        }
    }
}
