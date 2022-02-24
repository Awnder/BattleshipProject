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
            return "Agent Smith";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();

            if (attackHistory[attackGrid.x, attackGrid.y] == 'C' || attackHistory[attackGrid.x, attackGrid.y] == 'B' ||
                attackHistory[attackGrid.x, attackGrid.y] == 'S' || attackHistory[attackGrid.x, attackGrid.y] == 'D' ||
                attackHistory[attackGrid.x, attackGrid.y] == 'P')
            {
                initialX = attackGrid.x;
                initialY = attackGrid.y;
                attackGrid.x--;
                sweepCounter = 1;
            }
            else if (sweepCounter > 0)
            {
                Sweep();
            }
            else
            {
                RandomShot(rng);
            }

            while (attackGrid.x < 0 || attackGrid.x > 9 || attackGrid.y < 0 || attackGrid.y > 9)
            {
                Sweep();
            }
            if (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
            {
                if (sweepCounter == 4)
                {
                    RandomShot(rng);
                    sweepCounter = 0;
                }
                else
                {
                    Sweep();
                }
            }
            return attackGrid;
        }

        private void RandomShot(Random rng)
        {
            attackGrid.x = rng.Next(0, 10);
            attackGrid.y = rng.Next(0, 10);
            while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
            {
                attackGrid.x = rng.Next(0, 10);
                attackGrid.y = rng.Next(0, 10);
            }
        }

        private void Sweep()
        {
            if (sweepCounter == 1)
            {
                attackGrid.y = initialY + 1;
                attackGrid.x = initialX;
                sweepCounter++;

            }
            else if (sweepCounter == 2)
            {
                attackGrid.x = initialX + 1;
                attackGrid.y = initialY;
                sweepCounter++;
            }
            else if (sweepCounter == 3)
            {
                attackGrid.y = initialY - 1;
                attackGrid.x = initialX;
                sweepCounter = 4;
            }
            else
            {
                sweepCounter = 0;
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
            Random rng = new Random();
            BattleshipFleet myFleet = new BattleshipFleet();
            myFleet.Carrier = new ShipPosition(0, rng.Next(0, 4), ShipRotation.Horizontal); //Will only randomy place in top left side
            myFleet.Battleship = new ShipPosition(0, rng.Next(5, 8), ShipRotation.Horizontal); //Will only randomly place in bottom left side
            myFleet.Destroyer = new ShipPosition(rng.Next(5, 9), rng.Next(0, 3), ShipRotation.Vertical); //will only randomly place in top right side
            myFleet.Submarine = new ShipPosition(rng.Next(5, 9), rng.Next(4, 7), ShipRotation.Vertical); //will randomly place in bottom right side
            myFleet.PatrolBoat = new ShipPosition(rng.Next(0, 9), 9, ShipRotation.Horizontal); //will only randomly place in the bottom row beteween spots 0-8

            return myFleet;


        }
    }
}
