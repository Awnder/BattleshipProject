using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;

        char searchMode = 'H';
        int originx;
        int originy;
        char nsew;

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
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

            attackGrid.y = 0;
            attackGrid.x = 0;

            originx = -1;
            originy = -1;
            nsew = 'N';

            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Enemy";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            if (searchMode == 'A')
            {

                if (attackHistory[attackGrid.x, attackGrid.y] == 'M')
                {
                    if (nsew == 'N')
                    {
                        if (attackGrid.x < 9)
                        {
                            nsew = 'E';
                            attackGrid.x = originx + 1;
                            attackGrid.y = originy;
                        }
                        else if (attackGrid.y < 9)
                        {
                            nsew = 'S';
                            attackGrid.x = originx;
                            attackGrid.y = originy + 1;
                        }
                        else if (attackGrid.x > 0)
                        {
                            nsew = 'W';
                            attackGrid.x = originx - 1;
                            attackGrid.y = originy;
                        }
                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = originx;
                            attackGrid.y = originy;
                            nsew = 'N';
                        }


                    }
                    else if (nsew == 'E')
                    {
                        if (attackGrid.y < 9)
                        {
                            nsew = 'S';
                            attackGrid.x = originx;
                            attackGrid.y = originy + 1;
                        }
                        else if (attackGrid.x > 0)
                        {
                            nsew = 'W';
                            attackGrid.x = originx - 1;
                            attackGrid.y = originy;
                        }
                        else
                        {
                            nsew = 'H';
                            attackGrid.x = originx;
                            attackGrid.y = originy;
                            nsew = 'N';
                        }
                    }
                    else if (nsew == 'S')
                    {
                        if (attackGrid.x > 0)
                        {
                            nsew = 'W';
                            attackGrid.x = originx - 1;
                            attackGrid.y = originy;
                        }
                        else
                        {
                            nsew = 'H';
                            attackGrid.x = originx;
                            attackGrid.y = originy;
                            nsew = 'N';
                        }
                    }

                    else if (nsew == 'W')
                    {
                        nsew = 'H';
                        attackGrid.x = originx;
                        attackGrid.y = originy;
                        nsew = 'N';
                    }
                }

                else
                {
                    if (nsew == 'N')
                    {
                        if (attackGrid.y > 0)
                        {
                            attackGrid.y -= 1;
                        }
                        else
                        {
                            nsew = 'E';
                            attackGrid.x = originx + 1;
                            attackGrid.y = originy;
                        }
                    }
                    else if (nsew == 'E')
                    {
                        if (attackGrid.x < 9)
                        {
                            attackGrid.x += 1;
                        }
                        else
                        {
                            nsew = 'S';
                            attackGrid.x = originx;
                            attackGrid.y = originy + 1;
                        }
                    }
                    else if (nsew == 'S')
                    {
                        if (attackGrid.y < 9)
                        {
                            attackGrid.y += 1;
                        }
                        else
                        {
                            nsew = 'W';
                            attackGrid.x = originx - 1;
                            attackGrid.y = originy;
                        }
                    }
                    else if (nsew == 'W')
                    {
                        if (attackGrid.x > 0)
                        {
                            attackGrid.x -= 1;
                        }
                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = originx;
                            attackGrid.y = originy;
                            nsew = 'N';
                        }
                    }
                }
            }
            if (searchMode == 'H')
            {
                while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                {
                    if (attackGrid.y % 2 == 0)
                    {
                        if (attackGrid.x < 8)
                        {
                            attackGrid.x += 2;
                        }
                        else
                        {
                            attackGrid.x = 1;
                            attackGrid.y += 1;
                        }
                    }

                    else
                    {
                        if (attackGrid.x < 8)
                        {
                            attackGrid.x += 2;
                        }
                        else
                        {
                            attackGrid.x = 0;
                            attackGrid.y += 1;

                        }
                    }
                }
            }
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if (report == '\0')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'M';
            }
            else if (report == 'S' || report == 'P' || report == 'D' || report == 'C' || report == 'B')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'H';
                if (searchMode != 'A')
                {
                    originx = attackGrid.x;
                    originy = attackGrid.y;
                }
                searchMode = 'A';
            }

        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier = new ShipPosition(5, 0, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(7, 0, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(9, 0, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(3, 0, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(1, 4, ShipRotation.Horizontal);

            return myFleet;
        }
    }
}