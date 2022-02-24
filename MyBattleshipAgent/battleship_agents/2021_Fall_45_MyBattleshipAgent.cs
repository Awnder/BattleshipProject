using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng = new Random();
        int Carrier;
        int Battleship;
        int Destroyer;
        int Submarine;
        int PTBoat;


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
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "AI & Sons";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            //if a hit has been made, center on last hit, move left 1 square and fire, if miss, center again and move up one and fire,
            //if miss, center again and move right 1 and fire, else, center again and move one down and fire, else if another hit,
            //center on most recent hit, if left or right moves resulted in a hit, move one right and fire, if miss move left one from first hit and fire,
            //if miss, return to random target aquistion, if up or down moves resulted in a hit, move one up and fire,
            //if miss move one down from first hit and fire, if miss return to random target aquisition, if hit again loop respectively
            //you are aware of what ships types you are hitting but not if they are destroyed
            //miss = '\0'
            //destroyer = 'D'
            //etc...


            if (attackHistory[attackGrid.x, attackGrid.y] != 'U')
            {//if C hit detected; less than 5 C hits; strike up; if miss; strike right; if miss; strike down; if miss; strike left; if hit; continue direction(not implemented); Carrier++
                if (attackHistory[attackGrid.x, attackGrid.y] == 'C')
                {
                    if (Carrier < 5)
                    {
                        attackGrid.x = attackHistory[attackGrid.x + 1, attackGrid.y]; //'C' + 1;
                        attackGrid.y = attackHistory[attackGrid.x, attackGrid.y]; //'C';
                    }
                    else if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'U')
                    {
                        attackGrid.y = attackHistory[attackGrid.x, attackGrid.y - 1]; //'C' - 1;
                        attackGrid.x = attackHistory[attackGrid.x, attackGrid.y]; //'C';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y - 1] != 'U')
                    {
                        attackGrid.y = attackHistory[attackGrid.x, attackGrid.y + 1]; //'C' + 1;
                        attackGrid.x = attackHistory[attackGrid.x, attackGrid.y]; //'C';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y + 1] != 'U')
                    {
                        attackGrid.x = attackHistory[attackGrid.x - 1, attackGrid.y]; //'C' - 1;
                        attackGrid.y = attackHistory[attackGrid.x, attackGrid.y]; //'C';
                    }
                    else
                    {
                        attackGrid.x = rng.Next(10);
                        attackGrid.y = rng.Next(10);
                    }

                    Carrier++;
                    return attackGrid;
                }
                if (attackHistory[attackGrid.x, attackGrid.y] == 'B')
                {
                    if (Battleship < 4)
                    {
                        attackGrid.x = 'B' + 1;
                        attackGrid.y = 'B';
                    }
                    else if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'U')
                    {
                        attackGrid.y = 'B' - 1;
                        attackGrid.x = 'B';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y - 1] != 'U')
                    {
                        attackGrid.y = 'B' + 1;
                        attackGrid.x = 'B';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y + 1] != 'U')
                    {
                        attackGrid.x = 'B' - 1;
                        attackGrid.y = 'B';
                    }
                    else
                    {
                        attackGrid.x = rng.Next(10);
                        attackGrid.y = rng.Next(10);
                    }

                    Battleship++;
                    return attackGrid;

                }
                if (attackHistory[attackGrid.x, attackGrid.y] == 'D')
                {
                    if (Destroyer < 3)
                    {
                        attackGrid.x = 'D' + 1;
                        attackGrid.y = 'D';
                    }
                    else if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'U')
                    {
                        attackGrid.y = 'D' - 1;
                        attackGrid.x = 'D';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y - 1] != 'U')
                    {
                        attackGrid.y = 'D' + 1;
                        attackGrid.x = 'D';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y + 1] != 'U')
                    {
                        attackGrid.x = 'D' - 1;
                        attackGrid.y = 'D';
                    }
                    else
                    {
                        attackGrid.x = rng.Next(10);
                        attackGrid.y = rng.Next(10);
                    }

                    Destroyer++;
                    return attackGrid;

                }
                if (attackHistory[attackGrid.x, attackGrid.y] == 'S')
                {
                    if (Submarine < 3)
                    {
                        attackGrid.x = 'S' + 1;
                        attackGrid.y = 'S';
                    }
                    else if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'U')
                    {
                        attackGrid.y = 'S' - 1;
                        attackGrid.x = 'S';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y - 1] != 'U')
                    {
                        attackGrid.y = 'S' + 1;
                        attackGrid.x = 'S';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y + 1] != 'U')
                    {
                        attackGrid.x = 'S' - 1;
                        attackGrid.y = 'S';
                    }
                    else
                    {
                        attackGrid.x = rng.Next(10);
                        attackGrid.y = rng.Next(10);
                    }

                    Submarine++;
                    return attackGrid;

                }
                if (attackHistory[attackGrid.x, attackGrid.y] == 'P')
                {
                    if (PTBoat < 2)
                    {
                        attackGrid.x = 'P' + 1;
                        attackGrid.y = 'P';
                    }
                    else if (attackHistory[attackGrid.x + 1, attackGrid.y] != 'U')
                    {
                        attackGrid.y = 'P' - 1;
                        attackGrid.x = 'P';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y - 1] != 'U')
                    {
                        attackGrid.y = 'P' + 1;
                        attackGrid.x = 'P';
                    }
                    else if (attackHistory[attackGrid.x, attackGrid.y + 1] != 'U')
                    {
                        attackGrid.x = 'P' - 1;
                        attackGrid.y = 'P';
                    }
                    else
                    {
                        attackGrid.x = rng.Next(10);
                        attackGrid.y = rng.Next(10);
                    }

                    PTBoat++;
                    return attackGrid;

                }

                if (attackGrid.x < 0 || attackGrid.x > 9 || attackGrid.y < 0 || attackGrid.y > 9)
                {
                    do
                    {
                        attackGrid.x = rng.Next() % 10;
                        attackGrid.y = rng.Next() % 10;
                    } while (attackHistory[attackGrid.x, attackGrid.y] != 'U');
                }



            }
            return attackGrid;
        }


        public override void DamageReport(char report)
        {
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

            myFleet.Carrier = new ShipPosition(0, 0, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(5, 9, ShipRotation.Horizontal);
            myFleet.Destroyer = new ShipPosition(4, 0, ShipRotation.Horizontal);
            myFleet.Submarine = new ShipPosition(9, 1, ShipRotation.Vertical);

            if(rng.Next() % 2 == 1)
            {
                myFleet.PatrolBoat = new ShipPosition(5, 5, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.PatrolBoat = new ShipPosition(5, 5, ShipRotation.Vertical);
            }


            return myFleet;
        }
    }
}
