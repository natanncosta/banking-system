using System;
using BankingSystem.Library;

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
            Console.Clear();
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
            BackToMenu();
        }

        private static void ShowOne()
        {
            Customer customer = GetCustomer();

            int p = 10;
            Console.WriteLine(
                    $"{"CPF".PadRight(p)}{"| Nome".PadRight(p)}{"| RG".PadRight(p)}{"| Endereço".PadRight(p)}");
            Console.WriteLine($"{customer.CPF.PadRight(p)}{customer.Name.PadRight(p)}{customer.RG.PadRight(p)}{customer.Address.PadRight(p)}");
        }

        private static void Create()
        {
            Console.Write("Digite o CPF: ");
            string cpf = Console.ReadLine();

            Customer customer = _crud.GetCustomer(cpf);
            if (customer != null)
            {
                HandleError("Já há um cliente cadastrado neste número de CPF!");
                BackToMenu();
            }

            Console.Write("Digite o nome do cliente: ");
            string name = Console.ReadLine();

            Console.Write("Digite o RG: ");
            string rg = Console.ReadLine();

            Console.Write("Digite o endereco do cliente: ");
            string address = Console.ReadLine();

            _crud.Add(new Customer(cpf: cpf, name: name, rg: rg, address: address));

            HandleSuccess("Cliente cadastrado com sucesso!!");
        }

        private static void Edit()
        {
            Customer customer = GetCustomer();

            Console.Write("Digite o nome atualizado: ");
            customer.Name = Console.ReadLine();
            Console.Write("Digite o RG atualizado: ");
            customer.RG = Console.ReadLine();
            Console.Write("Digite o endereco atualizado: ");
            customer.Address = Console.ReadLine();

            _crud.Update(customer);

            HandleSuccess("Informacões do cliente atualizadas com sucesso!");
        }

        private static void ShowAll()
        {
            int p = 10;
            Console.WriteLine(
                    "CPF".PadRight(p)
                    + "| Nome".PadRight(p)
                    + "| RG".PadRight(p)
                    + "| Endereço".PadRight(p));

            var customers = _crud.GetAll();
            foreach (var c in customers)
            {
                Customer customer = c.Value;
                Console.WriteLine(
                        customer.CPF.PadRight(p)
                        + customer.Name.PadRight(p)
                        + customer.RG.PadRight(p)
                        + customer.Address.PadRight(p));
            }
        }
        static string ReadCpf()
        {
            Console.Write("Digite o CPF do cliente: ");
            return Console.ReadLine();
        }

        public static Customer GetCustomer()
        {
            string cpf = ReadCpf();
            Customer customer = _crud.GetCustomer(cpf);
            if (customer == null)
            {
                HandleError("Cliente não encontrado!");
            }

            return customer;
        }

        private static void Delete()
        {
            _crud.Delete(GetCustomer());
            HandleSuccess("Cliente excluido com sucesso.");
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
