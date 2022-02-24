using System;

namespace Battleship

/*
 * Battleship
 *  Plays battleship againts bots
 *  
 *  file: Fall_MaxMcCullough_BattleshipAgent.cs
 *  author: Max McCullough
 *
 */
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
            attackGrid.x = 0;
            attackGrid.y = 0;

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
            return "AI";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            //strategy: fire at random until a ship is hit, then shoot one spot to the right.

            //looks for ship 'C'

            if (attackHistory[attackGrid.x, attackGrid.y] == 'C' && attackGrid.x + 1 < 10 && attackGrid.y + 1 < 10)
            {
                if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'M' && attackHistory[attackGrid.x + 1, attackGrid.y] != 'C')
                {
                    attackGrid.x += 1;
                }
                else if (attackHistory[attackGrid.x, attackGrid.y] != 'M')
                {
                    attackGrid.x = rng.Next(10);
                    attackGrid.y = rng.Next(10);
                }

            }

            //looks for ship 'B'

            else if (attackHistory[attackGrid.x, attackGrid.y] == 'B' && attackGrid.x + 1 < 10)
            {
                if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'M' && attackHistory[attackGrid.x + 1, attackGrid.y] != 'B')
                {
                    attackGrid.x += 1;
                }
                else if (attackHistory[attackGrid.x, attackGrid.y] != 'M')
                {
                    attackGrid.x = rng.Next(10);
                    attackGrid.y = rng.Next(10);
                }
            }

            //looks for ship 'P'

            else if (attackHistory[attackGrid.x, attackGrid.y] == 'P' && attackGrid.x + 1 < 10)
            {
                if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'M' && attackHistory[attackGrid.x + 1, attackGrid.y] != 'P')
                {
                    attackGrid.x += 1;
                }
                else if (attackHistory[attackGrid.x, attackGrid.y] != 'M')
                {
                    attackGrid.x = rng.Next(10);
                    attackGrid.y = rng.Next(10);
                }
            }

            //looks for ship 'S'

            else if (attackHistory[attackGrid.x, attackGrid.y] == 'S' && attackGrid.x + 1 < 10)
            {
                if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'M' && attackHistory[attackGrid.x + 1, attackGrid.y] != 'S')
                {
                    attackGrid.x += 1;
                }
                else if (attackHistory[attackGrid.x, attackGrid.y] != 'M')
                {
                    attackGrid.x = rng.Next(10);
                    attackGrid.y = rng.Next(10);
                }
            }

            //looks for ship 'D'

            else if (attackHistory[attackGrid.x, attackGrid.y] == 'D' && attackGrid.x + 1 < 10)
            {
                if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'M' && attackHistory[attackGrid.x + 1, attackGrid.y] != 'D')
                {
                    attackGrid.x += 1;
                }
                else if (attackHistory[attackGrid.x, attackGrid.y] != 'M')
                {
                    attackGrid.x = rng.Next(10);
                    attackGrid.y = rng.Next(10);
                }
            }

            //if no ships were hit, fire at random without attacking a sqaure that has been hit in the past

            else if (attackHistory[attackGrid.x, attackGrid.y] != 'U')
            {
                for (int i = 0; i < attackHistory.GetLength(0); i++)
                {
                    for (int x = 0; x < attackHistory.GetLength(1); x++)
                    {
                        attackGrid.x += 1;
                    }
                }
            }

            //crash fix provided by prof.

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

            myFleet.Carrier = new ShipPosition(0, 5, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(5, 9, ShipRotation.Horizontal);
            myFleet.Destroyer = new ShipPosition(4, 0, ShipRotation.Horizontal);
            myFleet.Submarine = new ShipPosition(9, 5, ShipRotation.Vertical);

            //random allingmnet
            if(rng.Next() % 2 == 1)
            {
                myFleet.PatrolBoat = new ShipPosition(3, 7, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.PatrolBoat = new ShipPosition(3, 7, ShipRotation.Vertical);
            }
            return myFleet;
        }
    }
}
