using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brætspils_butikken
{
    public class BoardGame
    {
        //Public variables
        public string Title { get; set; }
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string GameType { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        public BoardGame() { }

        //Constructor
        public BoardGame(string title, string condition, decimal price, int quantity, string gameType, int minPlayers, int maxPlayers)
        {
            Title = title;
            Condition = condition;
            Price = price;
            Quantity = quantity;
            GameType = gameType;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
        }

        public override string ToString()
        {
            return $"{Title} ({GameType}) - {Condition}, Pris: {Price} kr, Antal: {Quantity}, Spillere: {MinPlayers}-{MaxPlayers}";
        }

    }
}
