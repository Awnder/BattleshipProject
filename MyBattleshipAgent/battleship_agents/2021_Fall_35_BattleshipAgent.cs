using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng = new Random(); //Putting random rng here so I can use it for my shooting strategy
        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            //Random rng = new Random();
        }

        public override void Initialize()
        {
             for (int i=0; i < attackHistory.GetLength(0); i++)
             { 
              for (int j = 0; j < attackHistory.GetLength(1); j++)
             {
             attackHistory[i, j] = '0';
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
            return "Spongebob";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            while (attackHistory[attackGrid.x, attackGrid.y] != '0') //(attackHistory[attackGrid.x, attackGrid.y != '0'])
            {
            attackGrid.x = rng.Next(10);
            attackGrid.y = rng.Next(10);
            }
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            //attackHistory[attackGrid.x, attackGrid.y] = report;
           // backslash 0 is a miss (when you put report in watch window)
            if(report == '\0')
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
            if (rng.Next() % 2 == 1)
            {
                myFleet.Carrier = new ShipPosition(8, 4, ShipRotation.Vertical);
            }
            else
            {
                myFleet.Carrier = new ShipPosition(3, 8, ShipRotation.Horizontal);
            }

            if (rng.Next() % 2 == 1)
            {
                myFleet.Battleship = new ShipPosition(5, 3, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.Battleship = new ShipPosition(1, 5, ShipRotation.Vertical);
            }

            if (rng.Next() % 2 == 1)
            {
                myFleet.Destroyer = new ShipPosition(1, 4, ShipRotation.Horizontal);
            }
            else 
            {
                myFleet.Destroyer = new ShipPosition(9, 0, ShipRotation.Vertical);
            }

            if (rng.Next() % 2 == 1)
            {
                myFleet.Submarine = new ShipPosition(0, 0, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.Submarine = new ShipPosition(3, 0, ShipRotation.Vertical);
            }

            if (rng.Next() % 2 == 1)
            {
                myFleet.PatrolBoat = new ShipPosition(5, 0, ShipRotation.Horizontal);
            }
            else 
            {
                myFleet.PatrolBoat = new ShipPosition(0, 1, ShipRotation.Vertical);
            }
            //myFleet.Submarine = new ShipPosition(4, 5, ShipRotation.Vertical);
            //myFleet.PatrolBoat = new ShipPosition(8, 3, ShipRotation.Horizontal);
            //myFleet.Carrier = new ShipPosition(0, 0, ShipRotation.Vertical);
            //myFleet.Battleship = new ShipPosition(1, 0, ShipRotation.Vertical);
            //myFleet.Destroyer = new ShipPosition(5, 7, ShipRotation.Vertical);

            return myFleet;
        }
    }
}

