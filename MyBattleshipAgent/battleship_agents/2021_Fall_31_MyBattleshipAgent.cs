using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng = new Random();


        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            attackGrid.x = 5;
            attackGrid.y = 5;
        }

        public override void Initialize()
        {
            for (int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for (int j = 0; j < attackHistory.GetLength(1); j++)
                {
                    attackHistory[i, j] = 'U'
                }
            }
            
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "AI The Great";
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

            myFleet.Carrier = new ShipPosition(0, 0, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(1, 0, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(2, 0, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(3, 0, ShipRotation.Vertical);

            if (rng.Next() % 2 == 1)
            {
                myFleet.PatrolBoat = new ShipPosition(5, 5, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.PatrolBoat = new ShipPosition(5, 5, ShipRotation.Vertical);
            }
            return myFleet;
        }
    }
}
