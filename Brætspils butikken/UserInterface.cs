using System;
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
                Console.WriteLine("4. Ændre et brætspil");
                Console.WriteLine("5. Afslut");
                Console.WriteLine("vælg en mulighed");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        inventory.AddBoardGame();
                        break;
                    case "2":
                        RemoveBoardGame();
                        break;
                    case "3":
                        inventory.ShowInventory();
                        break;
                    case "4":
                        EditBoardGame();
                        break;
                    case "5":
                        running = false;
                        Console.WriteLine("programmet afsluttes...");
                        break;
                    default:
                        Console.WriteLine("Ugyldigt valg, prøv igen");
                        break;
                }
            }
        }

        private void RemoveBoardGame()
        {
            Console.WriteLine("Indtast titlen på spillet du gerne vil fjerne: ");
            string title = Console.ReadLine();
            inventory.RemoveGame(title);
        }

        private void EditBoardGame()
        {
            Console.WriteLine("Indtast titlen på spillet du gerne vil redigere: ");
            string title = Console.ReadLine();
            inventory.EditGame(title);
        }
    }
}
