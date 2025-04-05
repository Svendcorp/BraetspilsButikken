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
        public string Id { get; set; } //Unique ID for each boardgame (only made in creation of instance)
        public string Title { get; set; }
        public string Condition { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string GameType { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }

        //Constructor to create new boardgame
        public BoardGame(string title, string condition, decimal price, string gameType, int minPlayers, int maxPlayers)
        {
            Id = Guid.NewGuid().ToString("N").Substring(0, 8); //generate unique ID
            Title = title;
            Condition = condition;
            Price = price;
            GameType = gameType;
            MinPlayers = minPlayers;
            MaxPlayers = maxPlayers;
        }

        public override string ToString()
        {
            return $"[{Id}]: {Title} ({GameType}) - {Condition}, Price: {Price} kr, Players: {MinPlayers}-{MaxPlayers}";
        }

    }

    public class RequestGame
    {
        //Public variable
        public string RequestTitle { get; set; }

        //Constructor to create a new request for a boardgame
        public RequestGame(string requestTitle)
        {
            RequestTitle = requestTitle;
        }

        public override string ToString()
        {
            return $"{RequestTitle}";
        }
    }


}
