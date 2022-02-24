using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] previousAttack;
        GridSquare Coord;

        public SuperCoolAgent()
        {
            previousAttack = new char[10, 10];
            Coord = new GridSquare();
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "S.S. Filthy Mango";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            Random rNum = new Random();
            int initialX = 0;
            int initialY = 0;
            int sweepCounter = 0;

            if (previousAttack[Coord.x, Coord.y] == 'C' || previousAttack[Coord.x, Coord.y] == 'B' ||
                previousAttack[Coord.x, Coord.y] == 'S' || previousAttack[Coord.x, Coord.y] == 'D' ||
                previousAttack[Coord.x, Coord.y] == 'P')
            {
                initialX = Coord.x;
                initialY = Coord.y;
                Coord.x--;
                sweepCounter = 1;
            }
            else if (sweepCounter > 0)
            {
                Sweep();
            }
            else
            {
                RandomShot(rNum);
            }

            while (Coord.x < 0 || Coord.x > 9 || Coord.y < 0 || Coord.y > 9)
            {
                Sweep();
            }
            if (previousAttack[Coord.x, Coord.y] != ShipType.None)
            {
                if (sweepCounter == 4)
                {
                    RandomShot(rNum);
                    sweepCounter = 0;
                }
                else
                {
                    Sweep();
                }
            }
            return Coord;
        }

        private void RandomShot(Random rNum)
        {
            Coord.x = rNum.Next(0, 10);
            Coord.y = rNum.Next(0, 10);
            while (previousAttack[Coord.x, Coord.y] != ShipType.None)
            {
                Coord.x = rNum.Next(0, 10);
                Coord.y = rNum.Next(0, 10);
            }
        }

        private void Sweep()
        {
            int initialX = 0;
            int initialY = 0;
            int sweepCounter = 0;
            if (sweepCounter == 1)
            {
                Coord.y = initialY + 1;
                Coord.x = initialX;
                sweepCounter++;

            }
            else if (sweepCounter == 2)
            {
                Coord.x = initialX + 1;
                Coord.y = initialY;
                sweepCounter++;
            }
            else if (sweepCounter == 3)
            {
                Coord.y = initialY - 1;
                Coord.x = initialX;
                sweepCounter = 4;
            }
            else
            {
                sweepCounter = 0;
            }
        }

        public override void DamageReport(char report)
        {
            int battleship = 0;
            int carrier = 0;
            int destroyer = 0;
            int patrol = 0;
            int submarine = 0;

            if (report == ShipType.None)
            {
                previousAttack[Coord.x, Coord.y] = 'X';
            }
            else
            {
                previousAttack[Coord.x, Coord.y] = report;
                    if (report == 'C')
                {
                    carrier--;
                }
                else if (report == 'B')
                {
                    battleship--;
                }
                else if (report == 'D')
                {
                    destroyer--;
                }
                else if (report == 'S')
                {
                    submarine--;
                }
                else if (report == 'P')
                {
                    patrol--;
                }

            }
            previousAttack[Coord.x, Coord.y] = report;
        }
        public override BattleshipFleet PositionFleet()
        {
            Random rng = new Random();
            BattleshipFleet Flock = new BattleshipFleet();

            Flock.Carrier = new ShipPosition(4, rng.Next(8, 9), ShipRotation.Horizontal);
            Flock.Battleship = new ShipPosition(0, rng.Next(4, 7), ShipRotation.Vertical);
            Flock.Destroyer = new ShipPosition(rng.Next(5, 9), 2, ShipRotation.Horizontal);
            Flock.Submarine = new ShipPosition(rng.Next(5, 9), 4, ShipRotation.Vertical);
            Flock.PatrolBoat = new ShipPosition(rng.Next(0, 9), 0, ShipRotation.Vertical);

            return Flock;
        }
    }
}
