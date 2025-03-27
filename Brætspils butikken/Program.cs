using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brætspils_butikken
{
    class Program
    {
        static void Main(string[] args)
        {
           Inventory inventory = new Inventory();
           inventory.LoadFromFile();

           UserInterface ui = new UserInterface();
           ui.Start();

           Console.ReadLine();
        }
    }
}


/* Info og kode:
 Create a Boardgame-Object
            BoardGame catan = new BoardGame("Catan", "Ny", 50.0m, 1, "Strategi");
            BoardGame monopoly = new BoardGame("Monopoly", "brugt", 30.0m, 1, "Familie");
            BoardGame chess01 = new BoardGame("Skak", "ny", 25.0m, 1, "Strategi");
            BoardGame chess02 = new BoardGame("Skak", "brugt", 15.0m, 1, "Strategi"); 
            //Add game to inventory
            inventory.Addgame(catan);
            inventory.Addgame(monopoly);
            inventory.Addgame(chess01);
            inventory.Addgame(chess02);
            
            //Show inventory
            inventory.ShowInventory();

            //Remove game
            inventory.RemoveGame("Monopoly");

            //Show inventory again
            inventory.ShowInventory();



*/