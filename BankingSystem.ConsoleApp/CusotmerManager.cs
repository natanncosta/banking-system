using System;
using BankingSystem.Library;
using BankingSystem.Library.Exceptions;

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
            CallOption(Program.ReadOption());
        }

        static void CallOption(string option)
        {
            // if (option == "1")
            //     Create();
            // else if (option == "2")
            //     Edit();
            // else if (option == "1")
            //     ShowAll();
            // else if (option == "1")
            //     ShowOne();
            // else if (option == "1")
            //     Delete();
            // else if (option == "1")
            //     Program.MainMenu();
            // else
            //     Menu();

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
                _customerCrud.GetCustomer(customer.CPF);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já há um cliente cadastrado neste número de CPF!");
                Console.ResetColor();
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

                _customerCrud.Add(customer);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Cliente cadastrado com sucesso!!");
                Console.ResetColor();
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

            _customerCrud.Update(old, newCustomer);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Informacões do cliente atualizadas com sucesso!");
            Console.ResetColor();

            BackToMenu();
        }

        private static void ShowAll()
        {
            Console.Clear();

            var customers = _customerCrud.GetAll();
            if (customers.Count == 0)
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

                foreach (var customer in customers)
                    Console.WriteLine(
                            $"{customer.CPF.PadRight(p)}{customer.Name.PadRight(p)}"
                            + $"{customer.RG.PadRight(p)}{customer.Address.PadRight(p)}");
            }

            BackToMenu();
        }

        public static Customer GetCustomer()
        {
            Console.Write("Digite o CPF do cliente: ");
            Customer customer = new(cpf: Console.ReadLine());

            try
            {
                return _customerCrud.GetCustomer(customer.CPF);
            }
            catch (CustomerNotFoundException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                BackToMenu();
            }

            return customer;
        }

        private static void Delete()
        {
            Console.Clear();

            Customer customer = GetCustomer();

            _customerCrud.Delete(customer);

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
