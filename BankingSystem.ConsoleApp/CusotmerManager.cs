using System;
using BankingSystem.Library;

namespace BankingSystem.ConsoleApp
{
    public static class CustomerManager
    {
        static CustomerCRUD _customerCrud = new();

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1 - Adicionar cliente");
            Console.WriteLine("2 - Editar cliente");
            Console.WriteLine("3 - Listar todos os clientes");
            Console.WriteLine("4 - Consultar cliente");
            Console.WriteLine("5 - Excluir cliente");
            Console.WriteLine("6 - Voltar");
            CallCustomerOption(Program.ReadOption());
        }

        static void CallCustomerOption(string option)
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
            string cpf = Console.ReadLine();

            Customer customer = _customerCrud.GetCustomer(cpf);
            if (customer is not null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já há um cliente cadastrado neste número de CPF!");
                Console.ResetColor();
                BackToMenu();
            }

            Console.Write("Digite o nome do cliente: ");
            string name = Console.ReadLine();
            Console.Write("Digite o RG: ");
            string rg = Console.ReadLine();
            Console.Write("Digite o endereco do cliente: ");
            string address = Console.ReadLine();

            Customer newCustomer = new(name, cpf, rg, address);
            _customerCrud.Add(newCustomer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cliente cadastrado com sucesso!!");
            Console.ResetColor();
            BackToMenu();
        }

        private static void Edit()
        {
            Console.Clear();

            Customer old = GetCustomer();
            Customer newClient = new(old.CPF);

            Console.Write("Digite o nome atualizado: ");
            newClient.Name = Console.ReadLine();
            Console.Write("Digite o RG atualizado: ");
            newClient.RG = Console.ReadLine();
            Console.Write("Digite o endereco atualizado: ");
            newClient.Address = Console.ReadLine();

            _customerCrud.Update(old, newClient);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Informacões do cliente atualizadas com sucesso!");
            Console.ResetColor();

            BackToMenu();
        }

        private static void ShowAll()
        {
            Console.Clear();

            var clients = _customerCrud.GetAll();
            if (clients.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Nenhum cliente registrado!");
                Console.ResetColor();
            }
            else
            {
                int p = 10;
                string header = "CPF".PadRight(p)
                                + "| Nome".PadRight(p)
                                + "| RG".PadRight(p)
                                + "| Endereço".PadRight(p);
                Console.WriteLine(header);

                foreach (var client in clients)
                    Console.WriteLine(
                            $"{client.CPF.PadRight(p)}{client.Name.PadRight(p)}"
                            + $"{client.RG.PadRight(p)}{client.Address.PadRight(p)}");
            }

            BackToMenu();
        }

        public static Customer GetCustomer()
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            Customer client = _customerCrud.GetCustomer(cpf);
            if (client is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cliente não encontrado.");
                Console.ResetColor();
                BackToMenu();
            }
            return client;
        }

        private static void Delete()
        {
            Console.Clear();

            Customer client = GetCustomer();

            _customerCrud.Delete(client);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cliente excluido com sucesso.");
            Console.ResetColor();

            BackToMenu();
        }

        static void BackToMenu()
        {
            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            CustomerManager.Menu();
        }
    }
}
