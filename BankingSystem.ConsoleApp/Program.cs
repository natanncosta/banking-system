﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace BankingSystem.ConsoleApp
{
    public class Program
    {

        static void Main(string[] args)
        {
            MainMenu();
        }

        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1 - Dados das contas");
            Console.WriteLine("2 - Dados dos clientes");
            Console.WriteLine("3 - Total de dinheiro em caixa");
            Console.WriteLine("4 - Total de tributos");
            Console.WriteLine("5 - Sair");
            CallSubMenus(ReadOption());
        }

        static void CallSubMenus(string option)
        {
            switch (option)
            {
                case "1":
                    AccountManager.Menu(); break;
                case "2":
                    ClientManager.Menu(); break;
                case "3":
                    AccountManager.TotalMoney(); break;
                case "4":
                    TotalTaxs(); break;
                case "5":
                    Exit(); break;
                default:
                    MainMenu(); break;
            }
        }

        static void TotalTaxs()
        {
            throw new NotImplementedException();
        }

        static void Exit()
        {
            Console.Write("\nEncerrando sistema");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(500);
                Console.Write(".");
            }
            Console.WriteLine();
            Environment.Exit(1);
        }

        public static string ReadOption()
        {
            Console.Write("=> ");
            return Console.ReadLine();
        }

        public static void BackToMainMenu()
        {
            Console.WriteLine("\nDite...");
            Console.ReadKey();
            MainMenu();
        }


    }
}
