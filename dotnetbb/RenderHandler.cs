using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    public class RenderHandler
    {
        public static void Render()
        {
            Tile[,] pitch = PitchHandler.pitch;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear();


            for (int y = 0; y < 14; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    if (pitch[x, y].marking == "endzone") Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (pitch[x, y].marking == "middle") Console.ForegroundColor = ConsoleColor.DarkGray;
                    else if (pitch[x, y].marking == "wide") Console.ForegroundColor = ConsoleColor.White;
                    else Console.ForegroundColor = ConsoleColor.Black;
                    if (pitch[x, y].playerOnThisTile == null && pitch[x, y].ballOnTile == null)
                    {
                        if (pitch[x, y].selected == false)
                        {
                            Console.Write("  .");
                        }
                        else
                        {
                            Console.Write("[ ]");
                        }
                    }
                    else if (pitch[x, y].playerOnThisTile != null)
                    {
                        if (pitch[x, y].playerOnThisTile.team == "home") Console.ForegroundColor = ConsoleColor.Blue;
                        else Console.ForegroundColor = ConsoleColor.Red;
                        if (pitch[x, y].selected == false)
                        {
                            Console.Write("|{0}|", pitch[x, y].playerOnThisTile.identificator);
                        }
                        else
                        {
                            Console.Write("[{0}]", pitch[x, y].playerOnThisTile.identificator);
                        }
                    }
                    else if (pitch[x, y].ballOnTile != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if (pitch[x, y].selected == false)
                        {
                            Console.Write("|B|");
                        }
                        else
                        {
                            Console.Write("[B]");
                        }

                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    
                }
                Console.Write(" {0} ", y);
                Console.WriteLine();
            }
            for (int x = 0; x < 25; x++)
            {
                if (x < 10) Console.Write(" {0} ", x);
                else Console.Write(" {0}", x);

            }
            Console.WriteLine();
        }
    }
}
