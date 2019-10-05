using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    public class Tile
    {
        public Player playerOnThisTile { get; set; }
        public Ball ballOnTile { get; set; }
        public bool selected { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public string marking { get; set; }
        public string onSide { get; set; }
        public Tile(Player putPlayerOnThisTile,int posiX,int posiY)
        {
            playerOnThisTile = putPlayerOnThisTile;
            ballOnTile = null;
            selected = false;
            posX = posiX;
            posY = posiY;
            ballOnTile = null;
            marking = "normal";

            if (posY == 3 || posY == 10) marking = "wide";
            else if (posX == 11 || posX == 12) marking = "middle";
            else if  (posX == 0 || posX == 24) marking = "endzone";

            if (posX < 12) onSide = "home";
            else onSide = "away";
        }

        public int GetHomeTackleZones()
        {

            int tackleZones = 0;
            for (int y = posY - 1; y <= posY + 1; y++)
            {
                for (int x = posX - 1; x <= posX + 1; x++)
                {
                    try
                    {
                        if (PitchHandler.pitch[x, y].playerOnThisTile != null && PitchHandler.pitch[x, y].playerOnThisTile?.team == "home" && PitchHandler.pitch[x,y].playerOnThisTile?.proned == false)
                        {
                            tackleZones++;
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.WriteLine("outside of bounds");
                    }
                }
            }
            return tackleZones;
        }

        public int GetAwayTackleZones()
        {
            int tackleZones = 0;
            for (int y = posY - 1; y <= posY + 1; y++)
            {
                for (int x = posX - 1; x <= posX + 1; x++)
                {
                    if (PitchHandler.pitch[x, y].playerOnThisTile != null && PitchHandler.pitch[x, y].playerOnThisTile?.team == "away" && PitchHandler.pitch[x, y].playerOnThisTile?.proned == false)
                    {
                        tackleZones++;
                    }
                }
            }
            return tackleZones;
        }
    }
}
