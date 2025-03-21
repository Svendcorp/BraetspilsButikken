using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace Brætspils_butikken
{
    public class Inventory
    {
        private List<BoardGame> games = new List<BoardGame>();
        private const string FilePath = "inventory.Json"; // Det er her data gemmes fra inventory. Det gemmes i en Json tekstfil. 

        public Inventory()
        {
            LoadFromFile(); // indlæses hver gang programmet starter.
        }


        //Add a new game to inventory
        
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

            // Opretter et nyt spil
            BoardGame game = new BoardGame(title, condition, price, quantity, gameType);

            games.Add(game);
            SaveToFile(); // saves the changes to File.
            Console.WriteLine($"Brætspillet '{game.Title}' er tilføjet til lageret.");
        }

        //Remove a game from inventory
        public void RemoveGame(string title)
        {
            BoardGame gameToRemove = games.Find(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (gameToRemove != null)
            {
                games.Remove(gameToRemove);
                Console.WriteLine($"Brætspillet '{title}' er fjernet fra lageret");
            }
            else
            {
                Console.WriteLine($"Brætspillet '{title}' blev ikke fundet i lageret");
            }
        }

        //Show all games in inventory
        public void ShowInventory()
        {
            if (games.Count == 0)
            {
                Console.WriteLine("Lageret er tomt");
                return;
            }

            Console.WriteLine("Brætspil i lageret:");
            foreach (var game in games)
            {
                Console.WriteLine(game);
            }
        }

        public void SaveToFile()
        {
            string Json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, Json);
        }


        private void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                string Json = File.ReadAllText(FilePath);
                games = JsonSerializer.Deserialize<List<BoardGame>>(Json) ?? new List<BoardGame>();
            }
        }


    }
}
