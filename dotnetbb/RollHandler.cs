using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    class RollHandler
    {
        public static Random generator = new Random();

        public static bool AgRoll(int modifier, Player player)
        {
            int tackleZones = 0;
            if (player.team == "home") tackleZones = PitchHandler.pitch[player.posX, player.posY].GetAwayTackleZones();
            else tackleZones = PitchHandler.pitch[player.posX, player.posY].GetHomeTackleZones();



            int roll = generator.Next(1, 7);
            Console.WriteLine("ag rolled: " + roll);
            Console.WriteLine("required roll: " + (7 - player.ag - modifier + tackleZones) + "+");
            Console.ReadKey();
            if (roll == 6) return true;
            if (roll == 1) return false;
            if (roll + modifier - tackleZones > 6 - player.ag) return true;

            return false;
        }

        public static void ScatterBall(int times, bool d6Scatter, Ball ball, bool kickoff)
        {
            PitchHandler.pitch[ball.PositionX, ball.PositionY].ballOnTile = null;
            for (int d8 = 0; d8 < times; d8++)
            {
                int d8roll = generator.Next(1, 9);
                Console.WriteLine("scatter rolled: "+d8roll);
                Console.ReadKey();
                if (d6Scatter)
                {
                    int d6roll = generator.Next(1, 7);
                    Console.WriteLine("kickoff distance rolled: " + d6roll);
                    Console.ReadKey();
                    switch (d8roll)
                    {
                        case 1:
                            ball.Move(-d6roll, -d6roll, "scatter");
                            break;
                        case 2:
                            ball.Move(0, -d6roll, "scatter");
                            break;
                        case 3:
                            ball.Move(d6roll, -d6roll, "scatter");
                            break;
                        case 4:
                            ball.Move(-d6roll, 0, "scatter");
                            break;
                        case 5:
                            ball.Move(d6roll, 0, "scatter");
                            break;
                        case 6:
                            ball.Move(-d6roll, d6roll, "scatter");
                            break;
                        case 7:
                            ball.Move(0, d6roll, "scatter");
                            break;
                        case 8:
                            ball.Move(d6roll, d6roll, "scatter");
                            break;
                    }

                    ScatterBall(1, false, ball, kickoff);

                }
                else
                {
                    switch (d8roll)
                    {
                        case 1:
                            ball.Move(-1, -1, "scatter");
                            break;
                        case 2:
                            ball.Move(0, -1, "scatter");
                            break;
                        case 3:
                            ball.Move(1, -1, "scatter");
                            break;
                        case 4:
                            ball.Move(-1, 0, "scatter");
                            break;
                        case 5:
                            ball.Move(1, 0, "scatter");
                            break;
                        case 6:
                            ball.Move(-1, 1, "scatter");
                            break;
                        case 7:
                            ball.Move(0, 1, "scatter");
                            break;
                        case 8:
                            ball.Move(1, 1, "scatter");
                            break;
                    }
                }
                RenderHandler.Render();
            }
        }

        public static void ThrowInBall(Ball ball,string direction)
        {
            int firstD6 = generator.Next(1, 7);
            int secondD6 = generator.Next(1, 7);
            Console.WriteLine("ball position: "+ball.PositionX+" "+ball.PositionY);
            Console.WriteLine("throw in rolled: "+firstD6+" and: "+secondD6);
            Console.ReadKey();
            switch (direction)
            {
                case "left":
                    Console.WriteLine("left");
                    if (firstD6 < 2) ball.Move(-secondD6, secondD6, "scatter");
                    else if (firstD6 > 4) ball.Move(-secondD6, -secondD6, "scatter");
                    else ball.Move(-secondD6, 0, "scatter");
                    break;
                case "right":
                    Console.WriteLine("right");
                    if (firstD6 < 2) ball.Move(secondD6, -secondD6, "scatter");
                    else if (firstD6 > 4) ball.Move(secondD6, secondD6, "scatter");
                    else ball.Move(secondD6, 0, "scatter");
                    break;
                case "down":
                    Console.WriteLine("down");
                    if (firstD6 < 2) ball.Move(secondD6, secondD6, "scatter");
                    else if (firstD6 > 4) ball.Move(-secondD6, secondD6, "scatter");
                    else ball.Move(0, secondD6, "scatter");
                    break;
                case "up":
                    Console.WriteLine("up");
                    if (firstD6 < 2) ball.Move(-secondD6, -secondD6, "scatter");
                    else if (firstD6 > 4) ball.Move(secondD6, -secondD6, "scatter");
                    else ball.Move(0, -secondD6, "scatter");
                    break;

            }
        }
    }
}
