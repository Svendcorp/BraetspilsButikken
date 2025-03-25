using System;
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

        }

        private void MenuDeleteGame()
        {

        }

        private void MenuEditGame()
        {
            Console.WriteLine("\n=== Rediger Brætspil ===");
            Console.WriteLine("\nIndtast titlen på det spil, du vil redigere:");
            string title = Console.ReadLine();
            inventory.EditGame(title);
        }

        private void MenuShowStorage()
        {
            Console.WriteLine();
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

        //=== Reservation Menu =====
        private void MenuReserveGame()
        {

        }

        private void MenuReservationList()
        {
            Console.WriteLine();
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
            Console.WriteLine("Save data before exit?");
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

       
        private void RemoveBoardGame()
        {
            Console.WriteLine("Indtast titlen på spillet du gerne vil fjerne: ");
            string title = Console.ReadLine();
            inventory.RemoveGame(title);
        
        }
    }
}
