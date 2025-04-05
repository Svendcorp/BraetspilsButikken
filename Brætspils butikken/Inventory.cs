using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

namespace Brætspils_butikken
{
    public class Inventory
    {
        private List<BoardGame> games = new List<BoardGame>();
        private const string FilePath = "inventory.Json"; // Det er her data gemmes fra inventory. Det gemmes i en Json tekstfil. 

        public Inventory()
        {
            LoadFromFile();
        }

        public void AddGame()
        {
            Console.Clear(); // Title
            Console.WriteLine("=== Add Game ===\n Insert title: ");
            loopTitle:
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Invalid input");
                goto loopTitle;
            }

            Console.Clear(); //Condition
            Console.WriteLine("=== Add Game ===\n Insert condition\n 1. Good\n 2. Decent \n 3. Bad\n");
            string condition;
            loopCondition:
            var conditionSwitch = Console.ReadKey(intercept: true).KeyChar;
            switch (conditionSwitch)
            {
                case '1':
                    condition = "Good";
                    break;
                case '2':
                    condition = "Decent";
                    break;
                case '3':
                    condition = "Bad";
                    Console.WriteLine("Condition: Bad");
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    goto loopCondition;
            }

            Console.Clear(); //Price
            Console.WriteLine("=== Add Game ===\n Insert price: ");
            loopPrice:
            string priceStr = Console.ReadLine();
            if (!decimal.TryParse(priceStr, out decimal price) || price < 0)
            {
                Console.WriteLine(" Invalid input");
                goto loopPrice;
            }
          
            Console.Clear(); //Genre
            Console.WriteLine("=== Add Game ===\n Insert Genre \n (Fx Strategy, Simulation, Familly)");
            loopGenre:
            string gameType = Console.ReadLine().ToLower().ToUpperInvariant();
            if (string.IsNullOrWhiteSpace(gameType))
            {
                Console.WriteLine("Invalid input");
                goto loopGenre;
            }

            Console.Clear(); //MinPlayers
            Console.WriteLine("=== Add Game ===\n Insert minimum players: ");
            loopMinPlayers:
            string minPlayersStr = Console.ReadLine();
            if (!int.TryParse(minPlayersStr, out int minPlayers) || minPlayers < 0)
            {
                Console.WriteLine(" Invalid input");
                goto loopMinPlayers;
            }


            Console.Clear(); //MaxPlayers
            Console.WriteLine("=== Add Game ===\n Insert maximum players: ");
            loopMaxPlayers:
            string maxPlayersStr = Console.ReadLine();
            if (!int.TryParse(maxPlayersStr, out int maxPlayers) || maxPlayers < 0)
            {
                Console.WriteLine(" Invalid input");
                goto loopMaxPlayers;
            }

            BoardGame game = new BoardGame(title, condition, price, gameType, minPlayers, maxPlayers);
            games.Add(game);
            SaveToFile();

            Console.Clear();
            Console.WriteLine($"\nGame '{game.Title}' has been added to inventory.");
            Console.ReadKey();
        }

        //Remove a game from inventory
        public void RemoveGame(string title)
        {
            var gameToRemove = games.FirstOrDefault(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (gameToRemove != null)
            {
                games.Remove(gameToRemove);
                SaveToFile();
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
                Console.WriteLine("Inventory is empty");
                return;
            }

            Console.WriteLine("\nBoardgames in inventory:");
            Console.WriteLine("----------------------------------------");
            foreach (var game in games)
            {
                Console.WriteLine(game);
            }
            Console.WriteLine("----------------------------------------");
        }

        public void EditGame(string title)
        {
            BoardGame gameToEdit = games.Find(g => g.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (gameToEdit != null)
            {
                Console.WriteLine($"=== Editing \"{title}\" ===");
                Console.WriteLine("Press Enter to keep the current value");

                Console.WriteLine($"Current condition: {gameToEdit.Condition}");
                Console.Write("New condition (New, Good, Bad): ");
                string newCondition = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newCondition))
                {
                    gameToEdit.Condition = newCondition;
                }

                Console.WriteLine($"Current price: {gameToEdit.Price}");
                Console.Write("New price: ");
                string newPriceStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newPriceStr) && decimal.TryParse(newPriceStr, out decimal newPrice))
                {
                    gameToEdit.Price = newPrice;
                }

                Console.WriteLine($"Current quantity: {gameToEdit.Quantity}");
                Console.Write("New quantity: ");
                string newQuantityStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newQuantityStr) && int.TryParse(newQuantityStr, out int newQuantity))
                {
                    gameToEdit.Quantity = newQuantity;
                }

                Console.WriteLine($"Current game type: {gameToEdit.GameType}");
                Console.Write("New game type: ");
                string newGameType = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newGameType))
                {
                    gameToEdit.GameType = newGameType;
                }

                Console.WriteLine($"Current minimum number of players: {gameToEdit.MinPlayers}");
                Console.Write("New minimum number of players: ");
                string newMinPlayersStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newMinPlayersStr) && int.TryParse(newMinPlayersStr, out int newMinPlayers))
                {
                    gameToEdit.MinPlayers = newMinPlayers;
                }

                Console.WriteLine($"Current maximum number of players: {gameToEdit.MaxPlayers}");
                Console.Write("New maximum number of players: ");
                string newMaxPlayersStr = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newMaxPlayersStr) && int.TryParse(newMaxPlayersStr, out int newMaxPlayers))
                {
                    gameToEdit.MaxPlayers = newMaxPlayers;
                }

                SaveToFile();
                Console.WriteLine($"Game '{title}' has been updated");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Game '{title}' not found in inventory");
                Console.ReadKey();
            }
        }

        public List<BoardGame> FindGame(string searchTerm) //Søg efter spil
        {
            List<BoardGame> results = new List<BoardGame>(); //Opretter en liste til at gemme resultaterne

                // Prøver at søge efter antal spillere
            if (int.TryParse(searchTerm, out int playerCount) && playerCount < 10) // Hvis searchTerm kan parses til et antal spillere og er mindre end 10
            {
                results = FindByPlayerCount(playerCount); 
            }
            // Prøver at søge efter pris
            else if (decimal.TryParse(searchTerm, out decimal price) && price > 10) // Hvis searchTerm kan parses til en pris og er større end 10
            {
                results = FindByPrice(price); 
            }
            
            else // Søger efter titel, gametype eller ID
            {
                results = FindByString(searchTerm);
            }

            return results;
        }

        private List<BoardGame> FindByPrice(decimal price) //Søg efter pris
        {
            List<BoardGame> results = new List<BoardGame>();
            var matches = games.Where(g => g.Price == price).ToList();
            
            Console.WriteLine($"Searching for price: {price}");
            Console.WriteLine($"Number of matches found: {matches.Count()}");
            
            foreach (var game in matches)
            {
                results.Add(game);
            }
            
            return results;
        }

        private List<BoardGame> FindByPlayerCount(int playerCount) //Søg efter antal spillere
        {
            List<BoardGame> results = new List<BoardGame>();
            var matches = games.Where(g => 
                playerCount >= g.MinPlayers && 
                playerCount <= g.MaxPlayers
            ).ToList();
            
            Console.WriteLine($"Searching for games with {playerCount} players");
            Console.WriteLine($"Number of matches found: {matches.Count()}");
            
            foreach (var game in matches)
            {
                results.Add(game);
            }
            
            return results;
        }

        private List<BoardGame> FindByString(string searchTerm) 
        {
            List<BoardGame> results = new List<BoardGame>();

            // Søg efter titel, spiltype eller ID
            var matches = games.Where(g =>
                g.Title.ToLower().Contains(searchTerm.ToLower()) ||
                g.GameType.ToLower().Contains(searchTerm.ToLower()) ||
                g.Id.ToString().IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0
            ).ToList();

            Console.WriteLine($"Searching for: {searchTerm}");
            Console.WriteLine($"Number of matches found: {matches.Count()}");

            foreach (var game in matches)
            {
                results.Add(game);
            }

            return results;
        }



        //=======Game Request Add/Remove ========//

        //Request List
        
        public List<RequestGame> GameRequests = new List<RequestGame>();

        //Request ADD
        public void GameRequest(string requestTitle)
        {

            GameRequests.Add(new RequestGame(requestTitle));
            SaveRequestToFile();

            Console.Clear();
            Console.WriteLine($"\nGame '{requestTitle}' has been added to game request.");
            Console.ReadKey();
        }


        //Show Request
        public void ShowRequest()
        {
            if (GameRequests.Count == 0)
            {
                Console.WriteLine("No new Requests");
                Console.ReadKey();

                return;
            }
            Console.Clear();

            GameRequests.Clear();
            LoadRequestFromFile();

            Console.WriteLine("\nRequests:");
            Console.WriteLine("----------------------------------------");
            foreach (var requestTitle in GameRequests)
            {
                Console.WriteLine(requestTitle);
            }
            Console.WriteLine("----------------------------------------");
            Console.ReadKey();

            
        }

        //Request Remove
        public void RemoveRequest(string title)
        {
            var requestToRemove = GameRequests.FirstOrDefault(g => g.RequestTitle.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (requestToRemove != null)
            {
                GameRequests.Remove(requestToRemove);
                SaveRequestToFile();

                Console.WriteLine($"Brætspillet '{title}' er fjernet fra lageret");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Brætspillet '{title}' blev ikke fundet i lageret");
                Console.ReadKey();
            }
        }



        //===== Request Save/load ======//
        //Save Request
        public void SaveRequestToFile()
        {
            string directoryPath = "TextFiles";
            string filePath = Path.Combine(directoryPath, "RequestFile.txt");
            try

            {
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                StreamWriter writer = new StreamWriter(filePath);

                foreach (var gameRequest in GameRequests)
                {
                    writer.WriteLine(gameRequest);
                }
                
                writer.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
        

        //Load Request
        public void LoadRequestFromFile()
        {

            string requestData;
            try
            {
                StreamReader reader = new StreamReader("TextFiles//RequestFile.txt");
                requestData = reader.ReadLine();

                while (requestData != null)
                {
                    requestData = reader.ReadLine();
                    GameRequests.Add(new RequestGame(requestData));
                }



                reader.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }
        



        //===== Save/load Game=====//
        public void SaveToFile()
        {
            try
            {
                Console.WriteLine($"Gemmer {games.Count} games to fil...");
                string json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
                Console.WriteLine("Data saved!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            if (!File.Exists(FilePath)) return; // Create file if it doesnt exit
            try
            {
                string json = File.ReadAllText(FilePath);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    Console.WriteLine("Loading games from file...");
                    games = JsonSerializer.Deserialize<List<BoardGame>>(json) ?? new List<BoardGame>();
                    Console.WriteLine($"Loaded {games.Count} games!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
                Console.WriteLine("Creating empty list.");
                games = new List<BoardGame>();
            }
        }
    }
}
