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
            Console.Clear(); //Title
            Console.WriteLine("=== Add Game ===\n Insert title: ");
            string title = Console.ReadLine();

            Console.Clear(); //Condition
            Console.WriteLine("=== Add Game ===\n Insert condition\n 1. Good\n 2. Decent \n3. Bad\n");
            string condition = "PlaceHolder";

            int conditionSwitch = int.Parse(Console.ReadLine());
            switch (conditionSwitch)
            {
                case 1:
                    condition = "Good";
                    break;
                case 2:
                    condition = "Decent";
                    break;
                case 3:
                    condition = "Bad";
                    Console.WriteLine("Condition: Bad");
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

            Console.Clear(); //Price
            Console.WriteLine("=== Add Game ===\n Insert price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Clear(); //Genre
            Console.WriteLine("=== Add Game ===\n Insert Genre (Fx Strategy, Simulation, Familly): ");
            string gameType = Console.ReadLine().ToLower().ToUpperInvariant();

            Console.WriteLine("=== Add Game ===\n Indtast minimum antal spillere: ");
            int minPlayers = int.Parse(Console.ReadLine());

            Console.WriteLine("=== Add Game ===\n Indtast maksimum antal spillere: ");
            int maxPlayers = int.Parse(Console.ReadLine());

            BoardGame game = new BoardGame(title, condition, price, gameType, minPlayers, maxPlayers);
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
            Console.WriteLine($"Gemmer {games.Count} spil til fil...");
            string json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
            Console.WriteLine("Data gemt!");
        }


        public void LoadFromFile()
        {
            if (!File.Exists(FilePath)) // Create file if it doesnt exit
            {
                Console.WriteLine("Filen findes ikke. Opretter tom liste.");
                games = new List<BoardGame>();
                return;
            }

            string json = File.ReadAllText(FilePath);
            if (!string.IsNullOrWhiteSpace(json))
            {
                Console.WriteLine("Indlæser data fra fil...");
                games = JsonSerializer.Deserialize<List<BoardGame>>(json) ?? new List<BoardGame>();
                Console.WriteLine($"Indlæst {games.Count} spil!");
            }
        }
    }
}
