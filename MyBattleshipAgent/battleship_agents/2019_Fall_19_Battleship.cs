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
            return "The Reisia Bot";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            Random random = new Random();
            int randomX = random.Next();
            attackGrid.x = random.Next(0,9);
            attackGrid.y = random.Next(0,9);
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            attackHistory[attackGrid.x, attackGrid.y] = report;
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier    = new ShipPosition(0, 0, ShipRotation.Horizontal);
            myFleet.Battleship = new ShipPosition(1, 5, ShipRotation.Vertical);
            myFleet.Destroyer  = new ShipPosition(5, 4, ShipRotation.Horizontal);
            myFleet.Submarine  = new ShipPosition(3, 3, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(9, 1, ShipRotation.Vertical);

            return myFleet;
        }
    }
}
