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

        public BoardGame() { }

        //Constructor
        public BoardGame(string title, string condition, decimal price, int quantity, string gameType)
        {
            Title = title;
            Condition = condition;
            Price = price;
            Quantity = quantity;
            GameType = gameType;
        }

        public override string ToString()
        {
            return $"{Title} ({GameType}) - {Condition}, Pris: {Price} kr, Antal: {Quantity}";
        }

    }
}
