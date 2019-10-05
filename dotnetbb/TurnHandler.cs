using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    public class TurnHandler
    {
        public static string state = "";
        public static bool turnOver = false;

        public static void touchdown(string team, Player player)
        {
            Console.WriteLine("nice one dude");
            Console.ReadKey();
        }
    }
}
