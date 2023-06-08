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
            throw new NotImplementedException();
        }

        private static void EditClient()
        {
            throw new NotImplementedException();
        }

        private static void ShowAllClients()
        {
            throw new NotImplementedException();
        }

        public static Client GetClient()
        {
            Client client = null;
            try
            {
                Console.Write("Digite o CPF do cliente: ");
                string cpf = Console.ReadLine();
                client = _clientCrud.GetClient(cpf);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
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
