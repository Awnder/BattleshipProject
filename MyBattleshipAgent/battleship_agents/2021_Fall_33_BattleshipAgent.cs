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
            for (int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for (int j = 0; j < attackHistory.GetLength(1); j++)
                {
                    attackHistory[i, j] = 'U';
                }
            }
            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Jake";
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

            myFleet.Carrier = new ShipPosition(8, 1, ShipRotation.Vertical);           //5  'C'
            myFleet.Battleship = new ShipPosition(2, 1, ShipRotation.Horizontal);      //4  'B'
            myFleet.Destroyer = new ShipPosition(2, 4, ShipRotation.Vertical);         //3  'D'
            myFleet.Submarine = new ShipPosition(9, 3, ShipRotation.Horizontal);       //3  'S'

            if (rng.Next() % 2 ==1)
            {
                myFleet.PatrolBoat = new ShipPosition(4, 6, ShipRotation.Horizontal);  //2  'P'
            }
            else
            {
                myFleet.PatrolBoat = new ShipPosition(4, 6, ShipRotation.Vertical);
            }
            return myFleet;
        }
    }
}
