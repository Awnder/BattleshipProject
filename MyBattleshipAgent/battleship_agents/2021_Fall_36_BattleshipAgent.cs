///*
//*BattleShip
//* The Game of BattleShip
    //*
    //*   file:   BattleShip.cs
    //* author: Kyle Stocek (The Water Polo Gang helped)
    //*/

    
    
    
    
    
    
using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;

        char searchObjective = 'H';
        int xAxis;
        int yAxis;
        char shots;

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

            xAxis = -1;
            yAxis = -1;
            shots = 'N';

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
            if (searchObjective == 'A')
            {
                if (attackHistory[attackGrid.x, attackGrid.y] == 'M')
                {
                    if (shots == 'N')
                    {

                        if (attackGrid.x < 9)
                        {
                            shots = 'E';
                            attackGrid.x = xAxis + 1;
                            attackGrid.y = yAxis;
                        }
                        else if (attackGrid.y < 9)
                        {
                            shots = 'S';
                            attackGrid.x = xAxis;
                            attackGrid.y = yAxis + 1;
                        }
                        else if (attackGrid.x > 0)
                        {
                            shots = 'W';
                            attackGrid.x = xAxis - 1;
                            attackGrid.y = yAxis;
                        }
                        else
                        {
                            searchObjective = 'H';
                            attackGrid.x = xAxis;
                            attackGrid.y = yAxis;
                            shots = 'N';
                        }
                    }
                    else if (shots == 'E')
                    {
                        if (attackGrid.y < 9)
                        {
                            shots = 'S';
                            attackGrid.x = xAxis;
                            attackGrid.y = yAxis + 1;
                        }
                        else if (attackGrid.x > 0)
                        {
                            shots = 'W';
                            attackGrid.x = xAxis - 1;
                            attackGrid.y = yAxis;
                        }
                        else
                        {
                            searchObjective = 'H';
                            attackGrid.x = xAxis;
                            attackGrid.y = yAxis;
                            shots = 'N';
                        }
                    }
                    else if (shots == 'S')
                    {
                        if (attackGrid.x > 0)
                        {
                            shots = 'W';
                            attackGrid.x = xAxis - 1;
                            attackGrid.y = yAxis;
                        }
                        else
                        {
                            searchObjective = 'H';
                            attackGrid.x = xAxis;
                            attackGrid.y = yAxis;
                            shots = 'N';
                        }
                    }
                    else if (shots == 'W')
                    {
                        searchObjective = 'H';
                        attackGrid.x = xAxis;
                        attackGrid.y = yAxis;
                        shots = 'N';
                    }
                }

                else
                {
                    if (shots == 'N')
                    {
                        if (attackGrid.y > 0)
                        {
                            attackGrid.y -= 1;
                        }
                        else
                        {
                            shots = 'E';
                            attackGrid.x = xAxis + 1;
                            attackGrid.y = yAxis;
                        }
                    }
                    else if (shots == 'E')
                    {
                        if (attackGrid.x < 9)
                        {
                            attackGrid.x += 1;
                        }
                        else
                        {
                            shots = 'S';
                            attackGrid.x = xAxis;
                            attackGrid.y = yAxis + 1;
                        }
                    }
                    else if (shots == 'S')
                    {
                        if (attackGrid.y < 9)
                        {
                            attackGrid.y += 1;
                        }
                        else
                        {
                            shots = 'W';
                            attackGrid.x = xAxis - 1;
                            attackGrid.y = yAxis;
                        }
                    }
                    else if (shots == 'W')
                    {
                        if (attackGrid.x > 0)
                        {
                            attackGrid.x -= 1;
                        }
                        else
                        {
                            searchObjective = 'H';
                            attackGrid.x = xAxis;
                            attackGrid.y = yAxis;
                            shots = 'N';
                        }
                    }
                }
            }
            if (searchObjective == 'H')
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
                if (searchObjective != 'A')
                {
                    xAxis = attackGrid.x;
                    yAxis = attackGrid.y;
                }
                searchObjective = 'A';
            }

        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            Random rng = new Random();

            if (rng.Next() % 2 == 0)
            {
                myFleet.Carrier = new ShipPosition(5, 0, ShipRotation.Vertical);
                myFleet.Battleship = new ShipPosition(7, 0, ShipRotation.Vertical);
                myFleet.Destroyer = new ShipPosition(9, 0, ShipRotation.Vertical);
                myFleet.Submarine = new ShipPosition(3, 0, ShipRotation.Vertical);
                myFleet.PatrolBoat = new ShipPosition(1, 4, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.Carrier = new ShipPosition(0, 1, ShipRotation.Vertical);
                myFleet.Battleship = new ShipPosition(6, 3, ShipRotation.Vertical);
                myFleet.Destroyer = new ShipPosition(0, 5, ShipRotation.Vertical);
                myFleet.Submarine = new ShipPosition(3, 2, ShipRotation.Vertical);
                myFleet.PatrolBoat = new ShipPosition(6, 0, ShipRotation.Horizontal);
            }
            return myFleet;
        }
    }
}

