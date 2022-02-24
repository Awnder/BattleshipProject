using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;

        Random rng = new Random();

        int lastX;
        int lastY;

        int counter = 1;

        char SearchType = 'A';
        char ShotType = '1';

        int[] startingpositions = { 0, 1, 2 };
        // make a loop in the initialize function that restarts at the beginning and cycles through the elements in the array

        // constructor (gets called when your agent is created)
        public SuperCoolAgent()
        {
            
            attackHistory = new char[10, 10];

            attackGrid = new GridSquare();
        }

        // gets called before every new game to reset the internal variables
        public override void Initialize()
        {
            for (int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for (int y = 0; y < attackHistory.GetLength(1); y++)
                {
                    // the loops fill the array with U's to indicate empty spots
                    attackHistory[i, y] = 'U';
                }
            }

            lastX = -2;
            lastY = 0;
            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Liam's Bot";
        }

        // tells you the name of the opponent you're playing 
        public override void SetOpponent(string opponent)
        {
            return;
        }

        // in order to shoot around the ship
        // if (attackGrid.x - 1 > 0 && attackGrid.x + 1 < 9 && attackGrid.y - 1 > 0 && attackGrid.y + 1 < 9)
        // {
        //      attackGrid.y -=1
        // return
        // }


        // where the attack lands (x and y coordinate)
        public override GridSquare LaunchAttack()
        {
            while (SearchType == 'A')
            {
                if (attackHistory[attackGrid.x, attackGrid.y] == 'B' || attackHistory[attackGrid.x, attackGrid.y] == 'C' || attackHistory[attackGrid.x, attackGrid.y] == 'P' || attackHistory[attackGrid.x, attackGrid.y] == 'S' || attackHistory[attackGrid.x, attackGrid.y] == 'D')
                {
                    SearchType = 'B';
                    break;
                }
                else
                {
                    SearchType = 'A';
                }
                //if (attackHistory[attackGrid.x, attackGrid.y] != '\0' && attackHistory[attackGrid.x, attackGrid.y] == 'U')
                {
                    while (ShotType == '1')
                    {
                        if (lastX + 2 < 10)
                        {
                            attackGrid.x = lastX + 2;
                        }
                        else
                        {
                            if (lastY + 1 > 9)
                            {
                                if (lastX == 9)
                                {
                                    attackGrid.x = 1;
                                }
                                else if (lastX == 8)
                                {
                                    attackGrid.x = 2;
                                }
                                attackGrid.y = 0;
                            }
                            else
                            {
                                attackGrid.y += 1;
                                attackGrid.x = -1;

                                lastX = attackGrid.x;
                                lastY = attackGrid.y;

                                ShotType = '2';
                                break;
                            }
                        }
                        lastX = attackGrid.x;
                        lastY = attackGrid.y;

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

                    while (ShotType == '2')
                    {
                        if (lastX + 2 < 10)
                        {
                            attackGrid.x = lastX + 2;
                        }
                        else
                        {
                            if (lastY + 1 > 9)
                            {
                                if (lastX == 9)
                                {
                                    attackGrid.x = 1;
                                }
                                else if (lastX == 8)
                                {
                                    attackGrid.x = 2;
                                }
                                attackGrid.y = 0;
                            }
                            else
                            {
                                attackGrid.y += 1;
                                attackGrid.x = 0;
                                ShotType = '1';
                                break;
                            }
                        }

                        lastX = attackGrid.x;
                        lastY = attackGrid.y;

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
                }

                break;

            }


            // if (SearchType != 'A' && attackHistory[attackGrid.x, attackGrid.y] != 'B' || attackHistory[attackGrid.x, attackGrid.y] != 'C' || attackHistory[attackGrid.x, attackGrid.y] != 'P' || attackHistory[attackGrid.x, attackGrid.y] != 'S' || attackHistory[attackGrid.x, attackGrid.y] != 'D')



            while (SearchType == 'B')
            {
                
                // North
                if (counter == 1)
                {

                    if (attackGrid.y - 1 >= 0)
                    {
                        attackGrid.y -= 1;
                        counter++;
                        return attackGrid;

                    }
                    else
                    {
                        counter++;
                    }
                }
                
                // East
                else if (counter == 2)
                {
                    if (attackGrid.x + 1 < 10)
                    {
                        attackGrid.x += 1;
                        counter++;
                        return attackGrid;

                    }
                    else
                    {
                        counter++;
                    }
                }

                // South
                else if (counter == 3)
                {
                    if (attackGrid.y + 1 < 10)
                    {
                        attackGrid.x -= 1;
                        attackGrid.y += 1;
                        counter++;
                        return attackGrid;

                    }
                    else
                    {
                        counter++;
                    }
                }

                // West
                else if (counter == 4)
                {
                    if (attackGrid.x - 1 >= 0)
                    {
                        attackGrid.y -= 1;
                        attackGrid.x -= 1;
                        counter++;
                        return attackGrid;
                    }
                    else
                    {
                        counter++;
                    }
                }

                else if (counter > 4)
                {
                    SearchType = 'A';
                    if (attackGrid.x > 0 || attackGrid.x < 9 || attackGrid.y > 0 || attackGrid.y < 9)
                    {
                        do
                        {
                            attackGrid.x = rng.Next() % 10;
                            attackGrid.y = rng.Next() % 10;
                        } while (attackHistory[attackGrid.x, attackGrid.y] != 'U');
                    }
                    counter = 1;
                    break;

                }


                else
                {
                    SearchType = 'A';
                    break;
                }
                if ((attackGrid.x > 0 && attackGrid.x < 9 && attackGrid.y > 0 && attackGrid.y < 9) && (attackHistory[attackGrid.x, attackGrid.y] == 'B' || attackHistory[attackGrid.x, attackGrid.y] == 'C' || attackHistory[attackGrid.x, attackGrid.y] == 'P' || attackHistory[attackGrid.x, attackGrid.y] == 'S' || attackHistory[attackGrid.x, attackGrid.y] == 'D'))
                {
                    counter -= 1;
                }

            }
            return attackGrid;
        }
            
       
           

        // prints out the result of where your shot hit
        public override void DamageReport(char report)
        {
            if (report == '\0')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'M';
            }
            else
            { 

                if (report == 'B')
                {
                    attackHistory[attackGrid.x, attackGrid.y] = 'B';
                }
                else if (report == 'P')
                {
                    attackHistory[attackGrid.x, attackGrid.y] = 'P';
                }
                else if (report == 'C')
                {
                    attackHistory[attackGrid.x, attackGrid.y] = 'C';
                }
                else if (report == 'D')
                {
                    attackHistory[attackGrid.x, attackGrid.y] = 'D';
                }
                else
                {
                    attackHistory[attackGrid.x, attackGrid.y] = 'S';
                }

            }
            attackHistory[attackGrid.x, attackGrid.y] = report;
        }


        // indicates where the fleet is positioned
        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier = new ShipPosition(2, 2, ShipRotation.Horizontal);
            myFleet.Battleship = new ShipPosition(8, 3, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(1, 6, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(8, 0, ShipRotation.Horizontal);

            Random rng1 = new Random();

            if (rng1.Next() % 2 == 0)
            {
                myFleet.Submarine = new ShipPosition(5, 7, ShipRotation.Vertical);
            }
            else
            {
                myFleet.Submarine = new ShipPosition(5, 7, ShipRotation.Horizontal);
            }
            
            return myFleet;
        }
    }
}

// THIS IS MY ORIGINAL CODE

//    public override GridSquare LaunchAttack()
//{
//    // x value step through the game board
//    if (lastX + 3 < 10)
//    {
//        attackGrid.x = lastX + 3;
//    }
//    else
//    {
//        if (lastY + 1 > 9)
//        {
//            if (lastX == 9)
//            {
//                attackGrid.x = 1;
//            }
//            else if (lastX == 8)
//            {
//                attackGrid.x = 2;
//            }
//            attackGrid.y = 0;
//        }
//        else
//        {
//            attackGrid.y += 1;
//            attackGrid.x = 0;
//        }
//        counter++;
//    }


// y value step through the game board