﻿using System;
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
        private Dictionary<string, BoardGame> gamesByTitle = new Dictionary<string, BoardGame>(StringComparer.OrdinalIgnoreCase);
        private const string FilePath = "inventory.Json"; // Det er her data gemmes fra inventory. Det gemmes i en Json tekstfil. 

        public Inventory()
        {
            LoadFromFile(); // indlæses hver gang programmet starter.
            UpdateDictionaries();
        }

        private void UpdateDictionaries()
        {
            gamesByTitle.Clear();
            foreach (var game in games)
            {
                gamesByTitle[game.Title] = game;
            }
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

            Console.WriteLine("=== Add Game ===\n Insert minimum number of players: ");
            int minPlayers = int.Parse(Console.ReadLine());

            Console.WriteLine("=== Add Game ===\n Insert maximum number of players: ");
            int maxPlayers = int.Parse(Console.ReadLine());

            BoardGame game = new BoardGame(title, condition, price, gameType, minPlayers, maxPlayers);
            games.Add(game);
            gamesByTitle[title] = game;
            SaveToFile(); // saves the changes to File.
            Console.WriteLine($"\nGame '{game.Title}' has been added to inventory.");
            Console.ReadKey();
        }

        //Remove a game from inventory
        public void RemoveGame(string title)
        {
            if (gamesByTitle.TryGetValue(title, out BoardGame gameToRemove))
            {
                games.Remove(gameToRemove);
                gamesByTitle.Remove(title);
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


        public List<BoardGame> FindGame(string searchTerm)
        {
            Console.Clear();
            List<BoardGame> ofGames = new List<BoardGame>();
            Console.WriteLine($"=== Search results for '{searchTerm}' ===\n");
            
            var results = games.Where(g => 
                g.Title.ToLower().Contains(searchTerm.ToLower()) ||
                g.GameType.ToLower().Contains(searchTerm.ToLower())
            ).ToList();

            if (results.Count == 0)
            {
                Console.WriteLine("No games found that match the search.");
            }
            else
            {
                foreach (var game in results)
                {
                    ofGames.Add(game);
                }
            }
            return ofGames;
        }

        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(games, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FilePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            if (File.Exists(FilePath))
            {
                try
                {
                    string json = File.ReadAllText(FilePath);
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        games = JsonSerializer.Deserialize<List<BoardGame>>(json) ?? new List<BoardGame>();
                        UpdateDictionaries();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading file: {ex.Message}");
                    games = new List<BoardGame>();
                }
            }
        }
    }
}
