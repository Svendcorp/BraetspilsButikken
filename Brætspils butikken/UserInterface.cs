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
            Console.WriteLine(" 5. Game Requests");
            Console.WriteLine();
            Console.WriteLine("=== Reservation Menu =====");
            Console.WriteLine(" 6. Reserve Game");
            Console.WriteLine(" 7. Reservation List");
            Console.WriteLine();
            Console.WriteLine(" 8. Exit");
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
                        MenuGameRequest();
                        MainMenu();
                        break;

                    case '6':
                        Console.Clear();
                        MenuReserveGame();
                        MainMenu();
                        break;

                    case '7':
                        Console.Clear();
                        MenuReservationList();
                        break;

                    case '8':
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
                Console.WriteLine(" 1. Print List");
                Console.WriteLine(" 2. Search");
                Console.WriteLine(" 3. Exit");
                Console.Write("\nChoose an option: ");

                var choice = Console.ReadKey(intercept: true);
                Console.WriteLine();

                switch (choice.KeyChar)
                {
                    case '1':
                        inventory.ShowInventory();
                        Console.WriteLine("\nPress a key to continue...");
                        Console.ReadKey();
                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine("=== Search for game ===");
                        Console.WriteLine("You can search for:");
                        Console.WriteLine("- Title or game type");
                        Console.WriteLine("- Price (e.g. 100)");
                        Console.WriteLine("- Player count (e.g. 4)");
                        Console.WriteLine("- GUID");
                        Console.Write("\nEnter search term: ");
                        string searchTerm = Console.ReadLine();

                        var results = inventory.FindGame(searchTerm);
                        
                        if (results.Count == 0)
                        {
                            Console.WriteLine("\nNo games found matching the search.");
                        }
                        else
                        {
                            Console.WriteLine($"\nFound {results.Count} games:");
                            Console.WriteLine(new string('-', 79));
                            foreach (var game in results)
                            {
                                // Justerer længden af titlen
                                string title = game.Title;
                                string gameType = game.GameType;
                                string price = $"{game.Price} kr.";
                                string playerCount = $"{game.MinPlayers}-{game.MaxPlayers} players";
                                string condition = game.Condition;

                                // Justerer output
                                Console.WriteLine($"{title,-25} | {gameType,-5} | {price,-5} | {playerCount,-10} | {condition,-5} | {game.Id,-5} | ");
                            }
                            Console.WriteLine(new string('-', 79));



                        }
                        Console.WriteLine("Press a key to continue...");
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




        //=== Game Requests =====
        public void MenuGameRequest()
        {
            Console.WriteLine("=== Game Request ===");
            Console.WriteLine(" 1. Insert Request");
            Console.WriteLine(" 2. Show Requests");
            Console.WriteLine(" 3. Delete Request");
            Console.WriteLine(" 4. Exit");

            bool keepGoing = true;
            while (keepGoing == true)
            {
                var choice = Console.ReadKey();
                switch (choice.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("=== Insert Request ===\n Insert title: ");
                    loopTitle:
                        string title = Console.ReadLine().ToLower();
                        if (string.IsNullOrWhiteSpace(title))
                        {
                            Console.WriteLine("Invalid input");
                            goto loopTitle;
                        }
                        
                        inventory.GameRequest(title);

                        keepGoing = false;

                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine("=== Show Requests ===");
                        inventory.ShowRequest();
                        keepGoing = false;

                        break;

                    case '3':
                        Console.Clear();
                        Console.WriteLine("=== Delete Request ===");
                        string requestTitle = Console.ReadLine().ToLower();
                        inventory.RemoveRequest(requestTitle);
                        keepGoing = false;

                        break;
                    
                    case '4': // Exit
                        keepGoing = false;
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;

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