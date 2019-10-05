using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetbb
{
    public class Player
    {
        public Ball HoldingBall { get; set; }
        public int ma { get; set; }
        public int st { get; set; }
        public int ag { get; set; }
        public int av { get; set; }
        public int posX { get; set; }
        public int posY { get; set; }
        public string team { get; set; }
        public bool proned { get; set; }
        public bool inTackleZone { get; set; }
        public IList<string> skills { get; set; }
        public string identificator { get; set; }

        public Player(int mov, int stg, int agi, int arm, int posiX, int posiY, string putTeam, string ident)
        {
            ma = mov;
            st = stg;
            ag = agi;
            av = arm;
            posY = posiY;
            posX = posiX;
            team = putTeam;
            proned = false;
            PitchHandler.pitch[posX, posY].playerOnThisTile = this;
            identificator = ident;
        }

        public void Move()
        {
            for (int movementLeft = ma; movementLeft > 0; movementLeft--)
            {
                ConsoleKey input = Console.ReadKey().Key;
                PitchHandler.pitch[posX, posY].playerOnThisTile = null;
                PitchHandler.pitch[posX, posY].selected = false;
                switch (input)
                {
                    case ConsoleKey.NumPad8:
                        if (PitchHandler.IsEmpty(posX, posY - 1)) posY--;
                        else movementLeft++;
                        break;
                    case ConsoleKey.NumPad6:
                        if (PitchHandler.IsEmpty(posX + 1, posY)) posX++;
                        else movementLeft++;
                        break;
                    case ConsoleKey.NumPad2:
                        if (PitchHandler.IsEmpty(posX, posY + 1)) posY++;
                        else movementLeft++;
                        break;
                    case ConsoleKey.NumPad4:
                        if (PitchHandler.IsEmpty(posX - 1, posY)) posX--;
                        else movementLeft++;
                        break;
                    default:
                        movementLeft++;
                        break;
                }

                Console.WriteLine(posX + ";" + posY);
                PitchHandler.pitch[posX, posY].playerOnThisTile = this;
                PitchHandler.pitch[posX, posY].selected = true;
                RenderHandler.Render();

                if (inTackleZone == true)
                {
                    if (!Dodge()) break;
                }

                if (PitchHandler.pitch[posX,posY].ballOnTile != null)
                {
                    Pickup(Program.ball);
                }

                if (team == "home")
                {
                    if (PitchHandler.pitch[posX, posY].GetAwayTackleZones() > 0) inTackleZone = true;
                    else inTackleZone = false;
                    if (PitchHandler.pitch[posX, posY].onSide == "away" && PitchHandler.pitch[posX, posY].marking == "endzone") TurnHandler.touchdown("home", this);
                }
                else
                {
                    if (PitchHandler.pitch[posX, posY].GetHomeTackleZones() > 0) inTackleZone = true;
                    else inTackleZone = false;
                    if (PitchHandler.pitch[posX, posY].onSide == "home" && PitchHandler.pitch[posX, posY].marking == "endzone") TurnHandler.touchdown("away", this);

                }
                

                
                Console.WriteLine("Movement left: "+movementLeft);
                Console.WriteLine("Holding ball: "+(HoldingBall!=null));
                Console.WriteLine("tile marking: "+PitchHandler.pitch[posX,posY].marking+" on side: "+PitchHandler.pitch[posX,posY].onSide);

            }
        }

        public void Prone()
        {
            proned = true;
            if (HoldingBall != null)
            {
                HoldingBall.PositionX = posX;
                HoldingBall.PositionY = posY;
                RollHandler.ScatterBall(1, false, HoldingBall, false);
            }
        }

        public bool Dodge()
        {
            if (RollHandler.AgRoll(+1, this))
            {
                return true;
            }
            else
            {
                Console.WriteLine("dodge failed");
                Prone();
                TurnHandler.turnOver = true;
                return false;
            }
        }

        public bool Pickup(Ball ball)
        {
            if (RollHandler.AgRoll(+1, this))
            {
                HoldingBall = ball;
                PitchHandler.pitch[posX, posY].ballOnTile = null;
                return true;
            }
            else
            {
                RollHandler.ScatterBall(1, false, ball, false);
                TurnHandler.turnOver = true;
                return false;
            }
        }

        public bool Catch(Ball ball, string type)
        {
            int modifier = 0;
            if (type == "scatter") modifier = -1;
            else if (type == "quick pass") modifier = +1;
            else if (type == "handover") modifier = +1;
            else if (type == "long pass") modifier = -1;
            else if (type == "long bomb") modifier = -1;

            if (RollHandler.AgRoll(+1+modifier, this))
            {
                HoldingBall = ball;
                PitchHandler.pitch[posX, posY].ballOnTile = null;
                return true;
            }
            else
            {
                RollHandler.ScatterBall(1, false, ball, false);
                TurnHandler.turnOver = true;
                return false;
            }
        }
        
    }
}
