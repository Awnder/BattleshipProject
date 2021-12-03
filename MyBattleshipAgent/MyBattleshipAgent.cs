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

        public override void Initialize()
        {
            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Andrew's Bot";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            attackGrid.x = 5;
            attackGrid.y = 5;
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            attackHistory[attackGrid.x, attackGrid.y] = report;
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier = new ShipPosition(0, 0, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(1, 0, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(2, 0, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(3, 0, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(4, 0, ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
