using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "S.S Moby Dick";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();
            attackGrid.x = rng.Next(0, 10);
            attackGrid.y = rng.Next(0, 10);
            while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
            {
                attackGrid.x = rng.Next(0, 10);
                attackGrid.y = rng.Next(0, 10);
            }
            return attackGrid;
        }

        public void sweep()
        {
            if (sweepCounter == 1)
            {
                attackGrid.x = initialHitx + 1;
                attackGrid.y = initialHity + 1;
                sweepCounter++;
            }
            else if (sweep == 2)
            {
                attackGrid.x = initialHitx + 1;
                attackGrid.y = initialHity + 1;
                sweepCounter++;
            }
        }
        public override void DamageReport(char report)
        {
            if (report == ShipType.None)
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'X';
            }
            else
            {
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();
            Random rng = new Random();

            myFleet.Carrier = new ShipPosition(0, rng.Next(0,4), ShipRotation.Horizontal);
            myFleet.Battleship = new ShipPosition(1, rng.Next(4,9), ShipRotation.Horizontal);
            myFleet.Destroyer = new ShipPosition(rng.Next(8,9), rng.Next(0,4), ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(rng.Next(5,7), rng.Next(5,7), ShipRotation.Vertical) ;
            myFleet.PatrolBoat = new ShipPosition(rng.Next(1, 2), rng.Next(0, 3), ShipRotation.Horizontal);
            
            return myFleet;
        }
    }
}
