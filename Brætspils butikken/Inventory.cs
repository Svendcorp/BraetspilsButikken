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


        //add a new game to inventory
        public void Addgame(BoardGame game)
        {
            games.Add(game);
            SaveToFile(); // saves the changes to File.
            Console.WriteLine($"Bræspillet '(game.Title)' er tilføjet til lageret.");
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
