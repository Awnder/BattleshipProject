using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        int patrolLives = 2;
        int submarineLives = 3;
        int destroyerLives = 3;
        int battleshipLives = 4;
        int carrierLives = 5;
        int initialX = 0;
        int initialY = 0;
        int sweepCounter = 0;

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
            return "Ben"; 
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
                if (report == 'C')
                {
                    carrierLives--;
                }
                else if (report == 'B')
                {
                    battleshipLives--;
                }
                else if (report == 'D')
                {
                    destroyerLives--;
                }
                else if (report == 'S')
                {
                    submarineLives--;
                }
                else if (report == 'P')
                {
                    patrolLives--;
                }

            }
            attackHistory[attackGrid.x, attackGrid.y] = report;
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();
            Random rng = new Random();

            myFleet.Carrier    = new ShipPosition(rng.Next(0, 5), rng.Next(5, 6), ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(rng.Next(5, 10), rng.Next(5, 6), ShipRotation.Vertical);
            myFleet.Destroyer  = new ShipPosition(rng.Next(3, 6), rng.Next(1, 2), ShipRotation.Vertical);
            myFleet.Submarine  = new ShipPosition(rng.Next(6, 7), rng.Next(0, 5), ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(rng.Next(1, 2), rng.Next(0, 3), ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
