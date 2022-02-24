/*
  * Battleship
  *   Battleship bot that can play the game Battleship
  *
  *   file: 2021_Fall_MaggieBrascia_MyBattleshipAgent.cs   
  *   author: Maggie Brascia
  */

using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng;
        bool searchingShot;
        int counter;
        int loopCount;
        GridSquare attackHistoryReset;

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            rng = new Random();
            attackGrid.x = 5;
            attackGrid.y = 4;
            searchingShot = false;
            counter = 0;
            loopCount = 0;
            attackHistoryReset = new GridSquare();

        }

        public override void Initialize()
        {
            //RESET GAME
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            rng = new Random();
            attackGrid.x = 5;
            attackGrid.y = 4;
            searchingShot = false;
            counter = 0;
            attackHistoryReset = new GridSquare();

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
            return "Maggie's Battleship";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
        
            //RESET COUNTER BACK TO 0
            if(counter == 2)
            {
                counter = 0;
            }

            //IF YOU HIT A SHIP, START SEARCHING FOR ADJACENT SQUARES
            if (searchingShot == true)
            {

                //RESET COUNTER
                if (counter == 0)
                {
                    attackHistoryReset = attackGrid;
                    counter++;
                }

                //RESET THE ATTACK GRID FOR EACH SEARCHING SHOT
                attackGrid = attackHistoryReset;


                // MOVE THE SHOT
                if (attackHistory[attackGrid.x, attackGrid.y] != 'M')
                {

                   if (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                    {

                        //SHOOT ONE TO THE RIGHT
                        attackGrid = attackHistoryReset;
                        attackGrid.x += 1;

                        //MAKE SURE THE SHOT IS IN BOUNDS
                        if(attackGrid.x > 9 || attackGrid.x < 0)
                        {
                            attackGrid = attackHistoryReset;
                        }

                            if(attackHistory[attackGrid.x, attackGrid.y] != 'U')
                        {

                            //SHOOT ONE TO THE LEFT
                            attackGrid = attackHistoryReset;
                            attackGrid.x -= 1;

                            if (attackGrid.x > 9 || attackGrid.x < 0)
                            {
                                attackGrid = attackHistoryReset;
                            }

                            if (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                            {
                                //SHOOT ONE BELOW
                                attackGrid = attackHistoryReset;
                                attackGrid.y += 1;

                                if (attackGrid.y > 9 || attackGrid.y < 0)
                                {
                                    attackGrid = attackHistoryReset;
                                }

                                if (attackHistory[attackGrid.x, attackGrid.y] != 'U')
                                {

                                    //SHOOT ONE ABOVE
                                    attackGrid = attackHistoryReset;
                                    attackGrid.y -= 1;

                                    if (attackGrid.y > 9 || attackGrid.y < 0)
                                    {
                                        attackGrid = attackHistoryReset;
                                    }

                                    //END THE SEARCH
                                    searchingShot = false;
                                    counter++;
                                    
                                    
                                    
                                }
                            }

                        }
                    }

                

                }
            
                
            }


            //RANDOMLY FIRING AT EVERY OTHER BOX UNTIL WE HIT A SHIP
            while (attackHistory[attackGrid.x, attackGrid.y] != 'U')

            {

                if (loopCount > 100000)
                {
                    break;
                }

                attackGrid.x = rng.Next(10);  
                attackGrid.y = rng.Next(10); 

                 while(attackGrid.x % 2 == 1 && attackGrid.y % 2 == 1) // ODD NUMBER 
                {
                    if(loopCount > 100000)
                    {
                        break;
                    }
                    while(attackGrid.y % 2 == 1) //WANT X TO BE ODD AND Y TO BE EVEN
                    {
                        attackGrid.y = rng.Next(10);
                        loopCount++;


                        //EVENTUALLY BREAK THE LOOP IF THERE ARE NO MORE SQUARES TO HIT,
                        //AND WE STILL HAVE NOT SUNK EVERY SHIP
                        if(loopCount > 100000)
                        {
                            break;
                        }

                    }
                }
                while(attackGrid.x % 2 == 0 && attackGrid.y % 2 == 0) // EVEN NUMBER
                {
                    if (loopCount > 100000)
                    {
                        break;
                    }

                    while (attackGrid.y % 2 == 0) //WANT X TO BE EVEN AND Y TO BE ODD
                    {

                        attackGrid.y = rng.Next(10);
                        loopCount++;


                        //EVENTUALLY BREAK THE LOOP
                        if (loopCount > 100000)
                        {
                            break;
                        }
                    }
                } 

            }


            //IF LOOP BREAKS, WE WANT TO HIT ALL THE REMAINING SQUARES
            while (attackHistory[attackGrid.x, attackGrid.y] != 'U')
            {
                attackGrid.x = rng.Next(10);
                attackGrid.y = rng.Next(10);
            }


            //MAKE SURE THE PROGRAM DOES NOT CRASH
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

            if(counter == 2)
            {
                searchingShot = false;
            }
            else if (report == 'B' || report == 'C' || report == 'D' || report == 'P' || report == 'S')
            {
                searchingShot = true;
            }

        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier = new ShipPosition(0, 1, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(5, 9, ShipRotation.Horizontal);
            myFleet.Destroyer = new ShipPosition(2, 0, ShipRotation.Horizontal);
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


