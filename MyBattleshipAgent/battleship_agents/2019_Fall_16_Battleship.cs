using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        int initialHitX = 0;
        int initialHitY = 0;
        int sweepCounter = 0;

        private int[] evenGuess = { 0, 2, 4, 6, 8 };
        private int[] oddGuess = { 1, 3, 5, 7, 9 };
        private int[] Lives = { 5, 4, 3, 3, 2, 0 };
        private int currentShip = 5;
        private int shotCounter = 0;

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
            return "Frisbee Bot";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            shotCounter++;

            if (attackHistory[attackGrid.x, attackGrid.y] == 'C' || attackHistory[attackGrid.x, attackGrid.y] == 'B' ||
                attackHistory[attackGrid.x, attackGrid.y] == 'D' || attackHistory[attackGrid.x, attackGrid.y] == 'S' ||
                attackHistory[attackGrid.x, attackGrid.y] == 'P')
            {
                if (sweepCounter == 1)
                    attackGrid.y--;
                else if (sweepCounter == 2)
                    attackGrid.x++;
                else if (sweepCounter == 3)
                    attackGrid.y++;
                else if (sweepCounter == 4)
                    attackGrid.x--;
                else
                {
                    if (attackHistory[attackGrid.x, attackGrid.y] == 'C')
                        currentShip = 0;
                    else if (attackHistory[attackGrid.x, attackGrid.y] == 'B')
                        currentShip = 1;
                    else if (attackHistory[attackGrid.x, attackGrid.y] == 'D')
                        currentShip = 2;
                    else if (attackHistory[attackGrid.x, attackGrid.y] == 'S')
                        currentShip = 3;
                    else if (attackHistory[attackGrid.x, attackGrid.y] == 'P')
                        currentShip = 4;

                    initialHitX = attackGrid.x;
                    initialHitY = attackGrid.y;
                    attackGrid.y = initialHitY - 1;
                    sweepCounter = 1;
                }
            }
            else if (Lives[currentShip] <= 0)
            {
                sweepCounter = 0;
                AttackGridSetRandomHashGrid();
                while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
                {
                    AttackGridSetRandomHashGrid();
                }
                return attackGrid;
            }
            else
            {
                SetGridToNextSweepPosition();
            }

            while ((attackGrid.x < 0 || attackGrid.x > 9) || (attackGrid.y < 0 || attackGrid.y > 9))
            {
                SetGridToNextSweepPosition();
            }
            if (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
            {
                SetGridToNextSweepPosition();
            }
            return attackGrid;
        }

        private void AttackGridSetRandomHashGrid()
        {
            Random rng = new Random();
            if (shotCounter < 50)
            {
                if (rng.Next(0, 2) == 0)
                {
                    attackGrid.x = evenGuess[rng.Next(0, 5)];
                    attackGrid.y = oddGuess[rng.Next(0, 5)];
                }
                else
                {
                    attackGrid.x = oddGuess[rng.Next(0, 5)];
                    attackGrid.y = evenGuess[rng.Next(0, 5)];
                }
            }
            else
            {
                attackGrid.x = rng.Next(0, 10);
                attackGrid.y = rng.Next(0, 10);
            }
        }

        private void SetGridToNextSweepPosition()
        {
            if (sweepCounter == 1)
            {
                attackGrid.x = initialHitX + 1;
                attackGrid.y = initialHitY;
                sweepCounter++;
            }
            else if (sweepCounter == 2)
            {
                attackGrid.x = initialHitX;
                attackGrid.y = initialHitY + 1;
                sweepCounter++;
            }
            else if (sweepCounter == 3)
            {
                attackGrid.x = initialHitX - 1;
                attackGrid.y = initialHitY;
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
                if (report == ShipType.Carrier)
                    Lives[0]--;
                else if (report == ShipType.Battleship)
                    Lives[1]--;
                else if (report == ShipType.Destroyer)
                    Lives[2]--;
                else if (report == ShipType.Submarine)
                    Lives[3]--;
                else if (report == ShipType.PatrolBoat)
                    Lives[4]--;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();
            Random rng = new Random();

            myFleet.Carrier = new ShipPosition(5, rng.Next(5, 10), ShipRotation.Horizontal);
            myFleet.Battleship = new ShipPosition(rng.Next(0, 5), rng.Next(5, 7), ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(rng.Next(0, 2), rng.Next(0, 5), ShipRotation.Horizontal);
            myFleet.Submarine = new ShipPosition(rng.Next(4, 8), rng.Next(0, 3), ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(rng.Next(8, 10), rng.Next(0, 4), ShipRotation.Vertical);

            return myFleet;
        }
    }
}
