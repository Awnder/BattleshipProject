using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory; //boardgame
        GridSquare attackGrid; //coordinates
        Random rng;

        public SuperCoolAgent() 
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            rng = new Random();
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "EmilyJ"; //playername
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {

            attackGrid.x = rng.Next(10);
            attackGrid.y = rng.Next(10);

            while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
            {

                attackGrid.x = rng.Next(10);
                attackGrid.y = rng.Next(10);

            }



            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if (report == ShipType.None)
            {
                attackHistory[attackGrid.x, attackGrid.y] = ('N');
            }
            else
            {


                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            int cx = rng.Next(5); int cy = 0;
            int bx = rng.Next(4); int by = rng.Next(5, 7);
            int dx = rng.Next(7 , 10); int dy = rng.Next(4);
            int sx = rng.Next(5, 8); int sy = rng.Next(6);
            int px = rng.Next(5 , 10); int py = rng.Next (8, 10);

            myFleet.Carrier    = new ShipPosition(cx, cy, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(bx, by, ShipRotation.Vertical);
            myFleet.Destroyer  = new ShipPosition(dx, dy, ShipRotation.Horizontal);
            myFleet.Submarine  = new ShipPosition(sx, sy, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(px, py, ShipRotation.Horizontal);

            return myFleet;
        }
    }

}
