using System;
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
            Console.WriteLine("3 - Listar todas as clientes");
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
            throw new NotImplementedException();
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

            Client client = GetClient();

            Console.Write("Digite o nome atualizado: ");
            client.Name = Console.ReadLine();
            Console.Write("Digite o RG atualizado: ");
            client.RG = Console.ReadLine();
            Console.Write("Digite o endereco atualizado: ");
            client.Address = Console.ReadLine();

            _clientCrud.Update(client);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Informacões do cliente atualizadas com sucesso!");
            Console.ResetColor();

            BackToClientMenu();
        }

        private static void ShowAllClients()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }


        static void BackToClientMenu()
        {
            Console.WriteLine("\nDite...");
            Console.ReadKey();
            ClientManager.Menu();
        }
    }
}
