using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    public class Ball
    {
        public int PositionY { get; set; }
        public int PositionX { get; set; }

        public Ball(int posX, int posY)
        {
            PositionY = posY;
            PositionX = posX;
            PitchHandler.pitch[PositionX, PositionY].ballOnTile = this;
        }

        public void Move(int xTiles, int yTiles, string reason)
        {
            string direction = "";
            if (PositionX > 26) direction = "left";
            else if (PositionX < 0) direction = "right";
            else if (PositionY > 15) direction = "up";
            else direction = "down";

            try
            {
                PositionY += yTiles;
                PositionX += xTiles;
                PitchHandler.pitch[PositionX, PositionY].ballOnTile = this;
            }
            catch (IndexOutOfRangeException)
            {
                if (TurnHandler.state == "kickoff")
                {
                    Console.WriteLine("reminder: fix touchback function for turnhandler");
                    Console.ReadKey();
                    //turnHandler.touchback(ball)
                }
                else
                {

                    RollHandler.ThrowInBall(this, direction);
                }
            }
            try { PitchHandler.pitch[PositionX - xTiles, PositionY - yTiles].ballOnTile = null; } catch (IndexOutOfRangeException) { }
            checkIfPickUp(reason);
        }

        public void PickUp(Player player)
        {
            if (player.posX == PositionX && player.posY == PositionY)
            {
                if (RollHandler.AgRoll(+1, player))
                {
                    PitchHandler.pitch[PositionX, PositionY].playerOnThisTile.HoldingBall = this;
                    PitchHandler.pitch[PositionX, PositionY].ballOnTile = null;
                }
                Console.WriteLine("Ball picked up");
                Console.ReadKey();
            }
            else
            {
                TurnHandler.turnOver = true;
                RollHandler.ScatterBall(1, false, this, false);
            }
        }
        void checkIfPickUp(string reason)
        {
            if (PitchHandler.pitch[PositionX, PositionY].playerOnThisTile != null) PitchHandler.pitch[PositionX, PositionY].playerOnThisTile.Catch(this, reason);
        }
    }
}
