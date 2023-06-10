using System;
using System.Collections.Generic;
using BankingSystem.Library;

namespace BankingSystem.ConsoleApp
{
    public static class ClientManager
    {
        static ClientCRUD _clientCrud = new();

        public static void Menu()
        {
            Console.Clear();
            Console.WriteLine("1 - Adicionar cliente");
            Console.WriteLine("2 - Editar cliente");
            Console.WriteLine("3 - Listar todos os clientes");
            Console.WriteLine("4 - Consultar cliente");
            Console.WriteLine("5 - Excluir cliente");
            Console.WriteLine("6 - Voltar");
            CallClientOption(Program.ReadOption());
        }

        static void CallClientOption(string option)
        {
            switch (option)
            {
                case "1":
                    CreateClient(); break;
                case "2":
                    EditClient(); break;
                case "3":
                    ShowAllClients(); break;
                case "4":
                    ShowClient(); break;
                case "5":
                    DeleteClient(); break;
                case "6":
                    Program.MainMenu(); break;
                default:
                    Menu(); break;
            }
        }

        private static void ShowClient()
        {
            Console.Clear();

            Client client = GetClient();

            int p = 10;
            Console.WriteLine(
                    $"{"CPF".PadRight(p)}{"| Nome".PadRight(p)}{"| RG".PadRight(p)}{"| Endereço".PadRight(p)}");
            Console.WriteLine($"{client.CPF.PadRight(p)}{client.Name.PadRight(p)}{client.RG.PadRight(p)}{client.Address.PadRight(p)}");

            BackToClientMenu();
        }

        private static void CreateClient()
        {
            Console.Clear();

            Console.Write("Digite o CPF: ");
            string cpf = Console.ReadLine();

            Client client = _clientCrud.GetClient(cpf);
            if (client is not null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Já há um cliente cadastrado neste número de CPF!");
                Console.ResetColor();
                BackToClientMenu();
            }

            Console.Write("Digite o nome do cliente: ");
            string name = Console.ReadLine();
            Console.Write("Digite o RG: ");
            string rg = Console.ReadLine();
            Console.Write("Digite o endereco do cliente: ");
            string address = Console.ReadLine();

            Client newClient = new(name, cpf, rg, address);
            _clientCrud.Add(newClient);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cliente cadastrado com sucesso!!");
            Console.ResetColor();
            BackToClientMenu();
        }

        private static void EditClient()
        {
            Console.Clear();

            Client old = GetClient();
            Client newClient = new(old.CPF);

            Console.Write("Digite o nome atualizado: ");
            newClient.Name = Console.ReadLine();
            Console.Write("Digite o RG atualizado: ");
            newClient.RG = Console.ReadLine();
            Console.Write("Digite o endereco atualizado: ");
            newClient.Address = Console.ReadLine();

            _clientCrud.Update(old, newClient);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Informacões do cliente atualizadas com sucesso!");
            Console.ResetColor();

            BackToClientMenu();
        }

        private static void ShowAllClients()
        {
            Console.Clear();

            HashSet<Client> clients = _clientCrud.GetAll();
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

            BackToClientMenu();
        }

        public static Client GetClient()
        {
            Console.Write("Digite o CPF do cliente: ");
            string cpf = Console.ReadLine();

            Client client = _clientCrud.GetClient(cpf);
            if (client is null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cliente não encontrado.");
                Console.ResetColor();
                BackToClientMenu();
            }
            return client;
        }

        private static void DeleteClient()
        {
            Console.Clear();

            Client client = GetClient();

            _clientCrud.Delete(client);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Cliente excluido com sucesso.");
            Console.ResetColor();

            BackToClientMenu();
        }

        static void BackToClientMenu()
        {
            Console.Write("\nPressione qualquer tecla...");
            Console.ReadKey();
            ClientManager.Menu();
        }
    }
}
