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

        public override void Initialize()
        {
            for (int i = 1; i < attackHistory.GetLength(0); i++)
            {
                for (int x = 0; x < attackHistory.GetLength(1); x++)
                {
                    attackHistory[i, x] = 'U';
                }
            }
            return;
        }

        public override string ToString()
        {
            return $"BattleshipBot '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Zayy";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
            {
                attackGrid.x = rng.Next(10);
                attackGrid.y = rng.Next(10);
            }
            return attackGrid;
        }
        public override void DamageReport(char report)
        {
            if (report == '\0')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'M';

            }
            else
            {
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Destroyer = new ShipPosition(8, 1, ShipRotation.Vertical);

            myFleet.Carrier = new ShipPosition(2, 4, ShipRotation.Horizontal);

            myFleet.Submarine = new ShipPosition(2, 1, ShipRotation.Vertical);

            myFleet.Battleship = new ShipPosition(9, 3, ShipRotation.Horizontal);

            if (rng.Next() % 2 == 1)
            {
                myFleet.PatrolBoat = new ShipPosition(5, 6, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.PatrolBoat = new ShipPosition(5, 6, ShipRotation.Vertical);
            }
            return myFleet;
        }

    }
}
