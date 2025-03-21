﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public void Start()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== Brætspil lagerstyring ===");
                Console.WriteLine("1. Tilføj nyt brætspil");
                Console.WriteLine("2. Fjern et brætspil");
                Console.WriteLine("3. Se lager");
                Console.WriteLine("4. Afslut");
                Console.WriteLine("vælg en mulighed");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBoardGame();
                        break;
                    case "2":
                        RemoveBoardGame();
                        break;
                    case "3":
                        inventory.ShowInventory();
                        break;
                    case "4":
                        running = false;
                        Console.WriteLine("programmet afsluttes...");
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg, prøv igen");
                        break;

                }

            }
        }

        public void AddBoardGame()
        {
            Console.WriteLine("Indtast titel: ");
            string title = Console.ReadLine();

            Console.WriteLine("Indtast stand (Ny, Brugt, Slidt): ");
            string condition = Console.ReadLine();

            Console.WriteLine("Indtast pris: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Indtast antal som tilføjes");
            int quantity = int.Parse(Console.ReadLine());

            Console.WriteLine("Indtast spiltype (Fx Strategi, Simulation, Familie): ");
            string gameType = Console.ReadLine();

            //Opretter nyt spil
            BoardGame game = new BoardGame(title, condition, price, quantity, gameType);
            
            //Tilføjer spillet til lageret
            inventory.Addgame(game);

            //Gemmer spillet i tekstfilen
            inventory.SaveToFile();


            Console.WriteLine("Brætspil tilføjet og gemt!");
        }

        private void RemoveBoardGame()
        {
            Console.WriteLine("Indtast titlen på spillet du gerne vil fjerne: ");
            string title = Console.ReadLine();
            inventory.RemoveGame(title);

        }
    }
}
