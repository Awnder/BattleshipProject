using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
        //plug in: user community creates new features that core program understands
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        char searchMode = 'H';
        int xHit;
        int yHit;
        char searchDirection;

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

            attackGrid.x = 0;
            attackGrid.y = 0;

            xHit = -1;
            yHit = -1;
            searchDirection = 'N';

            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Shrek";
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
                    if (searchDirection == 'N')
                    {
                        if (attackGrid.y + 1 <= 9)
                        {
                            searchDirection = 'S';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit + 1;
                        }

                        else if (attackGrid.x + 1 <= 9)
                        {
                            searchDirection = 'E';
                            attackGrid.x = xHit + 1;
                            attackGrid.y = yHit;
                        }

                        else if (attackGrid.x - 1 >= 0)
                        {
                            searchDirection = 'W';
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }

                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit;
                            searchDirection = 'N';
                        }
                    }

                    else if (searchDirection == 'S')
                    {
                        if (attackGrid.x + 1 <= 9)
                        {
                            searchDirection = 'E';
                            attackGrid.x = xHit + 1;
                            attackGrid.y = yHit;
                        }

                        else if (attackGrid.x - 1 >= 0)
                        {
                            searchDirection = 'W';
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }

                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit;
                            searchDirection = 'N';
                        }
                    }

                    else if (searchDirection == 'E')
                    {
                        if (attackGrid.x - 1 >= 0)
                        {
                            searchDirection = 'W';
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }

                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit;
                            searchDirection = 'N';
                        }
                    }

                    else if (searchDirection == 'W')
                    {
                        searchMode = 'H';
                        attackGrid.x = xHit;
                        attackGrid.y = yHit;
                        searchDirection = 'N';
                    }
                }

                else
                {
                    if (searchDirection == 'N')
                    {
                        if (attackGrid.y - 1 >= 0)
                        {
                            attackGrid.y -= 1;
                        }

                        else
                        {
                            searchDirection = 'S';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit + 1;
                        }
                    }

                    else if (searchDirection == 'S')
                    {
                        if (attackGrid.y + 1 <= 9)
                        {
                            attackGrid.y += 1;
                        }

                        else
                        {
                            searchDirection = 'E';
                            attackGrid.x = xHit + 1;
                            attackGrid.y = yHit;
                        }
                    }

                    else if (searchDirection == 'E')
                    {
                        if (attackGrid.x + 1 <= 9)
                        {
                            attackGrid.x += 1;
                        }

                        else
                        {
                            searchDirection = 'W';
                            attackGrid.x = xHit - 1;
                            attackGrid.y = yHit;
                        }
                    }

                    else if (searchDirection == 'W')
                    {
                        if (attackGrid.x -1 >= 0)
                        {
                            attackGrid.x -= 1;
                        }

                        else
                        {
                            searchMode = 'H';
                            attackGrid.x = xHit;
                            attackGrid.y = yHit;
                            searchDirection = 'N';
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

            else if (report == 'B' ||report == 'S' || report == 'D' || report == 'C' || report == 'P')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'H';

                if (searchMode != 'A')
                {
                    xHit = attackGrid.x;
                    yHit = attackGrid.y;
                }
                searchMode = 'A';
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier = new ShipPosition(9, 4, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(7, 0, ShipRotation.Horizontal);
            myFleet.Battleship = new ShipPosition(1, 1, ShipRotation.Horizontal);

            Random rng = new Random();

            if(rng.Next() % 2 == 0)
            {
                myFleet.Destroyer = new ShipPosition(2, 4, ShipRotation.Horizontal);
            }

            else
            {
                myFleet.Destroyer = new ShipPosition(2, 4, ShipRotation.Vertical);
            }

            Random x = new Random();

            if (x.Next() % 2 == 0)
            {
                myFleet.PatrolBoat = new ShipPosition(3, 8, ShipRotation.Vertical);
            }

            else
            {
                myFleet.PatrolBoat = new ShipPosition(3, 8, ShipRotation.Horizontal);
            }

                return myFleet;
        }
    }
}
