/*
 *  Battleship
 *  Bot that plays Battleship
 *
 *  file:   WinterFallsBattleship.cs
 *  author: Elize Chavez
 */


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

            attackGrid.x = rng.Next(10);
            attackGrid.y = rng.Next(10);
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Winter Falls ^-^";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            if (attackHistory[attackGrid.x, attackGrid.y] == 'U')       //search pattern
            {
                if (attackHistory[attackGrid.x, attackGrid.y] -1 =='A')
                {
                    attackGrid.x += 1;
                    attackGrid.y += 1;
                }
                else if (attackGrid.y < 9)
                {
                    attackGrid.y += 1;
                }
                else
                {
                    attackGrid.y = 0;
                    if (attackGrid.x < 9)
                    {
                        attackGrid.x += 1;
                    }
                    else
                    {
                        attackGrid.x = 0;
                    }
                }
            }

            //continuing attack [right, down]
            if (attackHistory[attackGrid.x, attackGrid.y] == 'M')
            {
                if (attackGrid.x == 9 || attackHistory[attackGrid.x + 1, attackGrid.y] != 'A')
                {
                    if (attackGrid.y < 9)
                    {
                        attackGrid.y += 1;
                    }
                    else
                    {
                        attackGrid.y = 0;
                        if (attackGrid.x < 9)
                        {
                            attackGrid.x += 1;
                        }
                        else
                        {
                            attackGrid.x = 0;
                        }
                    }
                }
                
                else
                {
                    if (attackGrid.x < 9)
                    {
                        attackGrid.x += 1;
                        if (attackGrid.y < 9)
                        {
                            attackGrid.y += 1;
                        }
                        else
                        {
                            attackGrid.y = 0;
                        }
                    }
                    else
                    {
                        attackGrid.x = 0;
                        if (attackGrid.y < 9)
                        {
                            attackGrid.y += 1;
                        }
                        else
                        {
                            attackGrid.y = 0;
                        }
                    }
                }
            }

            //attack (up,left)
            if (attackHistory[attackGrid.x, attackGrid.y] == 'A')
            {
                if (attackGrid.x > 0 && attackHistory [attackGrid.x - 1, attackGrid.y] == 'U')
                {
                    if (attackGrid.x > 0)
                    {
                        attackGrid.x -= 1;
                    }
                    else 
                    {
                        do
                        {
                            attackGrid.y += 1;
                        } while (attackHistory[attackGrid.x, attackGrid.y] =='M' || attackHistory[attackGrid.x, attackGrid.y] =='A');
                    }
                }
                else if (attackGrid.y > 0 && attackHistory[attackGrid.x, attackGrid.y - 1] == 'U')
                {
                    if (attackGrid.y > 0)
                    {
                        attackGrid.y -= 1;
                    }
                    else
                    {
                        if (attackGrid.x < 9)
                        {
                            attackGrid.x += 1;
                        }
                    }
                }
                else
                {
                    if (attackGrid.y < 9)
                    {
                        attackGrid.y += 1;
                    }
                    else
                    {
                        attackGrid.y = 0;
                        if (attackGrid.x < 9)
                        {
                            attackGrid.x += 1;
                        }
                        else
                        {
                            attackGrid.x = 0;
                        }
                    }
                }
            }
        
            if (attackGrid.x < 0 || attackGrid.x > 9 || attackGrid.y < 0 || attackGrid.y > 9)
            {
                do
                {
                    attackGrid.x = rng.Next() % 10;
                    attackGrid.y = rng.Next() % 10;
                } while (attackHistory[attackGrid.x, attackGrid.y] != 'U');
            }

            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if (report == 'C')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'A';
            }
            else if(report == 'B')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'A';
            }
            else if (report == 'D')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'A';
            }
            else if (report == 'S')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'A';
            }
            else if (report == 'P')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'A';
            }
            else if (report == '\0')
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

            myFleet.Carrier = new ShipPosition(rng.Next(0,2), 0, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(rng.Next(5,7), rng.Next(0,2), ShipRotation.Horizontal);
            myFleet.Destroyer = new ShipPosition(rng.Next(2, 7), rng.Next(3,4), ShipRotation.Horizontal);
            myFleet.Submarine = new ShipPosition(rng.Next(10), 5, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(rng.Next(9), rng.Next(8,9), ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
