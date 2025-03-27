﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Brætspils_butikken
{
    public class UserInterface
    {

        private Inventory inventory;

        public UserInterface()
        {
            inventory = new Inventory();
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n=== Admin Menu ===========");
            Console.WriteLine(" 1. Add Game");
            Console.WriteLine(" 2. Delete Game");
            Console.WriteLine(" 3. Edit Game");
            Console.WriteLine(" 4. Show Storage");
            Console.WriteLine();
            Console.WriteLine("=== Reservation Menu =====");
            Console.WriteLine(" 5. Reserve Game");
            Console.WriteLine(" 6. Reservation List");
            Console.WriteLine();
            Console.WriteLine(" 7. Exit");
            Console.WriteLine();
        }

        public void Start()
        {

            MainMenu();

            // Main Menu Switch
            bool keepGoingMenu = true;
            while (keepGoingMenu == true)
            {
                var choice = Console.ReadKey();
                Console.WriteLine();
                Console.Clear();

                switch (choice.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        MenuAddGame();
                        MainMenu();
                        break;

                    case '2':
                        Console.Clear();
                        MenuDeleteGame();
                        MainMenu();
                        break;

                    case '3':
                        Console.Clear();
                        MenuEditGame();
                        MainMenu();
                        break;

                    case '4':
                        Console.Clear();
                        MenuShowStorage();
                        MainMenu();
                        break;

                    case '5':
                        Console.Clear();
                        MenuReserveGame();
                        MainMenu();
                        break;

                    case '6':
                        Console.Clear();
                        MenuReservationList();
                        MainMenu();
                        break;

                    case '7':
                        Console.Clear();
                        MenuExit();
                        break;

                    default:
                        MainMenu();
                        Console.WriteLine("Invalid input");
                        break;

                }


            }
        }

        //=== Admin Menu ===========
        private void MenuAddGame()
        {
            inventory.AddGame();
        }

        private void MenuDeleteGame()
        {
            Console.WriteLine("=== Delete Game ===");
            Console.WriteLine("Enter the title of the game you want to delete: ");
            string title = Console.ReadLine();
            inventory.RemoveGame(title);
        }

        private void MenuEditGame()
        {
            Console.WriteLine("\n=== Edit Game ===");
            Console.WriteLine("\nEnter the title of the game you want to edit:");
            string title = Console.ReadLine();
            inventory.EditGame(title);
        }


        private void MenuShowStorage()
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                Console.Clear();
                Console.WriteLine("=== Show Storage ===");
                Console.WriteLine(" 1. Search");
                Console.WriteLine(" 2. Print List");
                Console.WriteLine(" 3. Exit");
                Console.Write("\nVælg en mulighed: ");

                var choice = Console.ReadKey(intercept: true);
                Console.WriteLine();

                switch (choice.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("=== Search for game ===");
                        Console.Write("Enter search term: ");
                        string searchTerm = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(searchTerm))
                        {
                            inventory.FindGame(searchTerm);
                        }
                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine("=== Storage ===");
                        inventory.ShowInventory();
                        Console.WriteLine("\nPress a key to continue...");
                        Console.ReadKey();
                        break;

                    case '3':
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Press a key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        //=== Reservation Menu =====
        private void MenuReserveGame()
        {
            Console.WriteLine("=== Reserve Game ===");

        }

        private void MenuReservationList()
        {
            Console.WriteLine("=== Reservation List ===");
            Console.WriteLine(" 1. Search");
            Console.WriteLine(" 2. Print List");

            bool keepGoing = true;
            while (keepGoing == true)
            {
                var choice = Console.ReadKey();

                if ('1' == choice.KeyChar)
                {
                    keepGoing = false;
                }
                else if ('2' == choice.KeyChar)
                {
                    keepGoing = false;
                }
                else
                {
                    Console.WriteLine(" Is an Invalid input");
                }
            }
        }

        //Exit
        private void MenuExit()
        {
            Console.WriteLine("=== Exit ===");
            Console.WriteLine(" Save data before exit?");
            Console.WriteLine(" 1. Save");
            Console.WriteLine(" 2. Dont Save");

            bool keepGoing = true;
            while (keepGoing == true)
            {
                var choice = Console.ReadKey();

                if ('1' == choice.KeyChar)
                {
                    Environment.Exit(0); // Exit Console
                }
                else if ('2' == choice.KeyChar)
                {
                    Environment.Exit(0); // Exit Console
                }
                else
                {
                    Console.WriteLine(" Is an Invalid input");
                }
            }
        }

    }
}
//test