using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
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
            return "RaeRae"; //player's nickname
        }

        //public override void SetOpponent(string opponent)       I had an old version of the formatting, so you told me to comment this out. 
        //{
        //    return;
        //}

        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();

            while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
            {
                attackGrid.x = rng.Next(0, 10);
                attackGrid.y = rng.Next(0, 10);
            }
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if(report == ShipType.None)
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'x';   
            }
            else
            {
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            Random rng = new Random();

            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier    = new ShipPosition(5, rng.Next(2, 3), ShipRotation.Horizontal);     //5 units
            myFleet.Battleship = new ShipPosition(rng.Next(8, 10), 7, ShipRotation.Vertical);     //4 units 
            myFleet.Destroyer  = new ShipPosition(1, rng.Next(7, 10), ShipRotation.Horizontal);     //3 units 
            myFleet.Submarine  = new ShipPosition(5, rng.Next(7, 9), ShipRotation.Horizontal);     //3 units 
            myFleet.PatrolBoat = new ShipPosition(rng.Next(3, 4), 4, ShipRotation.Vertical);     //2 units 

            return myFleet;
        }
    }
}
