using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    class PitchHandler
    {
        public static Tile[,] pitch = new Tile[25, 14];

        static PitchHandler()
        {
            EmptyPitch();
        }

        public static void EmptyPitch()
        {
            for (int y = 0; y < 14; y++)
            {
                for (int x = 0; x < 25; x++)
                {
                    pitch[x, y] = new Tile(null, x, y);
                }
            }
        }

        public static bool IsEmpty(int posX, int posY)
        {
            bool empty = false;
            try
            {
                empty = pitch[posX, posY].playerOnThisTile == null;
            }
            catch (IndexOutOfRangeException)
            {
                empty = false;
            }

            return empty;
        }
    }
}
