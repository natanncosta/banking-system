using System;
using BankingSystem.Library;
using BankingSystem.Library.Exceptions;

namespace BankingSystem.ConsoleApp
{
    public static class CustomerConsole
    {
        private static CustomerCRUD _crud = new CustomerCRUD();

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1 - Adicionar cliente");
            Console.WriteLine("2 - Editar cliente");
            Console.WriteLine("3 - Listar todos os clientes");
            Console.WriteLine("4 - Consultar cliente");
            Console.WriteLine("5 - Excluir cliente");
            Console.WriteLine("6 - Voltar");
            CallOption(Program.ReadOption());
        }

        static void CallOption(string option)
        {
            switch (option)
            {
                case "1":
                    Create(); break;
                case "2":
                    Edit(); break;
                case "3":
                    ShowAll(); break;
                case "4":
                    ShowOne(); break;
                case "5":
                    Delete(); break;
                case "6":
                    Program.MainMenu(); break;
                default:
                    Menu(); break;
            }
        }

        private static void ShowOne()
        {
            Console.Clear();

            Customer customer = GetCustomer();

            int p = 10;
            Console.WriteLine(
                    $"{"CPF".PadRight(p)}{"| Nome".PadRight(p)}{"| RG".PadRight(p)}{"| Endereço".PadRight(p)}");
            Console.WriteLine($"{customer.CPF.PadRight(p)}{customer.Name.PadRight(p)}{customer.RG.PadRight(p)}{customer.Address.PadRight(p)}");

            BackToMenu();
        }

        private static void Create()
        {
            Console.Clear();

            Console.Write("Digite o CPF: ");
            Customer customer = new Customer(cpf: Console.ReadLine());

            try
            {
                _crud.GetCustomer(customer.CPF);
                HandleError("Já há um cliente cadastrado neste número de CPF!");
                BackToMenu();
            }
            catch (CustomerNotFoundException)
            {
                Console.Write("Digite o nome do cliente: ");
                customer.Name = Console.ReadLine();

                Console.Write("Digite o RG: ");
                customer.RG = Console.ReadLine();

                Console.Write("Digite o endereco do cliente: ");
                customer.Address = Console.ReadLine();

                _crud.Add(customer);

                HandleSuccess("Cliente cadastrado com sucesso!!");
                BackToMenu();
            }
        }

        private static void Edit()
        {
            Console.Clear();

            Customer old = GetCustomer();
            Customer newCustomer = new(old.CPF);

            Console.Write("Digite o nome atualizado: ");
            newCustomer.Name = Console.ReadLine();
            Console.Write("Digite o RG atualizado: ");
            newCustomer.RG = Console.ReadLine();
            Console.Write("Digite o endereco atualizado: ");
            newCustomer.Address = Console.ReadLine();

            _crud.Update(old, newCustomer);

            HandleSuccess("Informacões do cliente atualizadas com sucesso!");

            BackToMenu();
        }

        private static void ShowAll()
        {
            Console.Clear();

            int p = 10;
            Console.WriteLine(
                    "CPF".PadRight(p)
                    + "| Nome".PadRight(p)
                    + "| RG".PadRight(p)
                    + "| Endereço".PadRight(p));

            var customers = _crud.GetAll();
            foreach (var customer in customers)
                Console.WriteLine(
                        customer.CPF.PadRight(p)
                        + customer.Name.PadRight(p)
                        + customer.RG.PadRight(p)
                        + customer.Address.PadRight(p));

            BackToMenu();
        }

        public static Customer GetCustomer()
        {
            Console.Write("Digite o CPF do cliente: ");
            Customer customer = new(cpf: Console.ReadLine());

            try
            {
                return _crud.GetCustomer(customer.CPF);
            }
            catch (CustomerNotFoundException e)
            {
                HandleError(e.Message);
                BackToMenu();
            }

            return customer;

        }

        private static void Delete()
        {
            Console.Clear();

            _crud.Delete(GetCustomer());
            HandleSuccess("Cliente excluido com sucesso.");

            BackToMenu();
        }

        private static void HandleError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void HandleWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void HandleSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void BackToMenu()
        {
            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            CustomerConsole.Menu();
        }
    }
}
