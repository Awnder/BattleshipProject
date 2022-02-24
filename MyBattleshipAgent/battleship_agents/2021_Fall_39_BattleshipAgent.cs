using System;

namespace Battleship
{
    public class TheCaptain : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng = new Random();


        public TheCaptain()
        {
            attackHistory = new char[10, 10];
            for (int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for (int j = 0; j < attackHistory.GetLength(1); j++)
                {
                    attackHistory[i, j] = 'U';
                }
            }
            attackGrid = new GridSquare();
            attackGrid.x = rng.Next(10);
            attackGrid.y = rng.Next(10);

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
            return "Captain Speering XXXV";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }
        public void ToError()
        {
            attackGrid.x = rng.Next(10);
            attackGrid.y = rng.Next(10);
        }

        public override GridSquare LaunchAttack()
        {
        

        int choice = rng.Next();
                if (choice % 2 == 0)
                {
                    while (attackHistory[attackGrid.x, attackGrid.y] != 'U') 
                    {
                       attackGrid.y += 1;
                        if (attackGrid.x == 8)
                        {
                            attackGrid.x += 1;
                            attackGrid.y = 0;
                        }
                    else if (attackGrid.x < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.y < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.x > 8)
                    {
                        ToError();
                    }
                    else if (attackGrid.y > 8)
                    {
                        ToError();
                    }
                }
                }
            else if (choice % 5 == 0)
            {
                while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                {
                    attackGrid.y += -1;
                    if (attackGrid.x == -1)
                    {
                        attackGrid.x += -1;
                        attackGrid.y = 7;
                    }
                    else if (attackGrid.x < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.y < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.x > 8)
                    {
                        ToError();
                    }
                    else if (attackGrid.y > 8)
                    {
                        ToError();
                    }
                }
            }


            else if (choice % 11 == 0)
                {
                    while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                    {
                        attackGrid.x += 1;
                        if (attackGrid.y == 8)
                        {
                            attackGrid.x = 0;
                            attackGrid.y += 1;

                        }
                       else if (attackGrid.x > 8)
                    {
                        ToError();
                    }
                    else if (attackGrid.y < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.y < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.y > 8)
                    {
                        ToError();
                    }
                }
                }
            else if (choice % 7 == 0)
            {
                while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                {
                    attackGrid.x += -1;
                    if (attackGrid.y == 0)
                    {
                        attackGrid.x = 7;
                        attackGrid.y += -1;

                    }
                    else if (attackGrid.x > 8)
                    {
                        ToError();
                    }
                    else if (attackGrid.y < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.x == 0)
                    {
                        ToError();
                    }
                   
                    else if (attackGrid.x < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.y > 8)
                    {
                        ToError();
                    }
                }
            }
            else if (choice % 3 == 0)
            {
                while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                {
                    attackGrid.x += 1;
                    attackGrid.y += 1;
                    if (attackGrid.x == 8)
                    {
                        attackGrid.x = rng.Next(10);
                        attackGrid.y = 0;
                    }
                    else if (attackGrid.x < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.y < 0)
                    {
                        ToError();
                    }
                    else if (attackGrid.x > 8)
                    {
                        ToError();
                    }
                    else if (attackGrid.y > 8)
                    {
                        ToError();
                    }
                }
            }

            else
                {
                    while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                    {
                        attackGrid.x = rng.Next(10);
                        attackGrid.y = rng.Next(10);
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
            attackHistory[attackGrid.x, attackGrid.y] = report;
            
        }
       

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();
            myFleet.Carrier = new ShipPosition(5, 1, ShipRotation.Vertical);
            if (rng.Next() % 2 == 0)
            {
                myFleet.Battleship = new ShipPosition(4, 8, ShipRotation.Horizontal);
            }
            else
            {
                myFleet.Battleship = new ShipPosition(5, 6, ShipRotation.Horizontal);
            }
            if (rng.Next() % 5 == 0)
            {
                myFleet.Destroyer = new ShipPosition(1, 0, ShipRotation.Vertical);
            }
            else
            {
                myFleet.Destroyer = new ShipPosition(7, 0, ShipRotation.Vertical);
            }
            if (rng.Next() % 5 == 0)
            {
                myFleet.Submarine = new ShipPosition(2, 7, ShipRotation.Vertical);
            }
            else
            {
                myFleet.Submarine = new ShipPosition(2, 7, ShipRotation.Horizontal);
            }
            if (rng.Next() % 2 == 0)
            { 
                myFleet.PatrolBoat = new ShipPosition(8, 3, ShipRotation.Horizontal);
              }
              else{
             myFleet.PatrolBoat = new ShipPosition(1, 4, ShipRotation.Horizontal);
          }        

                return myFleet;
        }
    }
}
