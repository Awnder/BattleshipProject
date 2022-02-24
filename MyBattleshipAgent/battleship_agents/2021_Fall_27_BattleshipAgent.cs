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
            return "Won Reasly";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        private Random myRng;
        myRng = new Random
       
        public override GridSquare LaunchAttack()
        {
            attackGrid.x = rng.Next(10);
            attackGrid.y = rng.Next(10);

            if (attackGrid.x < 0 || attackGrid.x > 9 || attackGrid.y < 0 || attackGrid.y > 9)
            {
                do
                {
                    attackGrid.x = rng.Next() % 10;
                    attackGrid.y = rng.Next() % 10;
                } while (attackHistory[attackGrid.x, attackGrid.y] != 'U');
            }

            return attackGrid;
        }


        public override void DamageReport(char report)
        {
            attackHistory[attackGrid.x, attackGrid.y] = report;
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier = new ShipPosition(3, 1, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(5, 0, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(2, 2, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(6, 5, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(8, 5, ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
