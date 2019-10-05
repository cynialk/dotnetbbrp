using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    class Program
    {   
        public static Ball ball = new Ball(1, 1);

        static void Main(string[] args)
        {
            RenderHandler.Render();
            RollHandler.ScatterBall(1, true, ball, false);
            Player lineman = new Player(999, 3, 0, 9, 0, 0, "away","L");
            Player lineman2 = new Player(6, 3, 3, 9, 5, 5, "home","L");
            Player lineman3 = new Player(6, 3, 3, 9, 3, 5, "home","L");
            lineman.Move();
            Console.ReadKey();
            lineman2.Move();
            lineman3.Move();

            

        }
    }
}
