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
        
        public void AddGame()
        {
            int Id = 15;

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

            Console.WriteLine("Indtast minimum antal spillere: ");
            int minPlayers = int.Parse(Console.ReadLine());

            Console.WriteLine("Indtast maksimum antal spillere: ");
            int maxPlayers = int.Parse(Console.ReadLine());

            BoardGame game = new BoardGame(Id, title, condition, price, quantity, gameType, minPlayers, maxPlayers);
            games.Add(game);
            SaveToFile(); // saves the changes to File.
            Console.WriteLine($"\nBrætspillet '{game.Title}' er tilføjet til lageret.");
            Console.ReadKey();
        }

        //Remove a game from inventory
        public void RemoveGame(string title)
        {
            BoardGame gameToRemove = games.Find(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (gameToRemove != null)
            {
                games.Remove(gameToRemove);
                Console.WriteLine($"Brætspillet '{title}' er fjernet fra lageret");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Brætspillet '{title}' blev ikke fundet i lageret");
                Console.ReadKey();
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

        public void EditGame(string title)
        {
            BoardGame gameToEdit = games.Find(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (gameToEdit != null)
            {
                Console.WriteLine($"=== Redigerer \"{title}\" ===");
                Console.WriteLine("Tryk Enter for at beholde den nuværende værdi");

                Console.WriteLine($"Nuværende stand: {gameToEdit.Condition}");
                Console.Write("Ny stand (Ny, God, Slidt): ");
                string newCondition = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newCondition))
                {
                    gameToEdit.Condition = newCondition;
                }

                Console.WriteLine($"Nuværende pris: {gameToEdit.Price}");
                Console.Write("Ny pris: ");
                string newPriceStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPriceStr) && decimal.TryParse(newPriceStr, out decimal newPrice))
                {
                    gameToEdit.Price = newPrice;
                }

                Console.WriteLine($"Nuværende antal: {gameToEdit.Quantity}");
                Console.Write("Nyt antal: ");
                string newQuantityStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newQuantityStr) && int.TryParse(newQuantityStr, out int newQuantity))
                {
                    gameToEdit.Quantity = newQuantity;
                }

                Console.WriteLine($"Nuværende spiltype: {gameToEdit.GameType}");
                Console.Write("Ny spiltype: ");
                string newGameType = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newGameType))
                {
                    gameToEdit.GameType = newGameType;
                }

                Console.WriteLine($"Nuværende minimum antal spillere: {gameToEdit.MinPlayers}");
                Console.Write("Nyt minimum antal spillere: ");
                string newMinPlayersStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newMinPlayersStr) && int.TryParse(newMinPlayersStr, out int newMinPlayers))
                {
                    gameToEdit.MinPlayers = newMinPlayers;
                }

                Console.WriteLine($"Nuværende maksimum antal spillere: {gameToEdit.MaxPlayers}");
                Console.Write("Nyt maksimum antal spillere: ");
                string newMaxPlayersStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newMaxPlayersStr) && int.TryParse(newMaxPlayersStr, out int newMaxPlayers))
                {
                    gameToEdit.MaxPlayers = newMaxPlayers;
                }

                SaveToFile();
                Console.WriteLine($"Brætspillet '{title}' er blevet opdateret");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Brætspillet '{title}' blev ikke fundet i lageret");
                Console.ReadKey();
            }
        }

        public void SaveToFile()
        {
            string Json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, Json);
        }


        public void LoadFromFile()
        {
            if (File.Exists(FilePath)) return;
             
                string Json = File.ReadAllText(FilePath);
            if (!string.IsNullOrWhiteSpace(Json))
            { 
                games = JsonSerializer.Deserialize<List<BoardGame>>(Json) ?? new List<BoardGame>();
            }

            if (games.Count > 0)
            {
                BoardGame.nextId = games.Max(g => g.Id) + 1;
            }
        }


    }
}
