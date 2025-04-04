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

           UserInterface ui = new UserInterface();
           ui.Start();

           Console.ReadLine();
        }
    }
}
