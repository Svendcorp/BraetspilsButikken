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
        public int Id { get; private set; } //Unique ID for each boardgame (only made in creation of instance)
        public string Title { get; set; }
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string GameType { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        internal static int nextId = 1; //keep tack of the next ID, starts from 1

        

        public BoardGame() { }

        //Constructor
        public BoardGame(string title, string condition, decimal price, string gameType, int minPlayers, int maxPlayers)
        {
            Id = nextId++;
            Title = title;
            Condition = condition;
            Price = price;
            GameType = gameType;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
        }

        public override string ToString()
        {
            return $"{Id}: {Title} ({GameType}) - {Condition}, Pris: {Price} kr, Spillere: {MinPlayers}-{MaxPlayers}";
        }

    }
}
