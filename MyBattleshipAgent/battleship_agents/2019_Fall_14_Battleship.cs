using System;

namespace BattleshipLib


{

    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        bool[,] shots;
        GridSquare attackGrid;

        int preX;
        int preY;

        int numOfShips;
        int shipIndex;
        int[] shipX;
        int[] shipY;

        int bHits;
        int cHits;
        int dHits;
        int sHits;
        int pHits;


        public SuperCoolAgent()
        {
            shots = new bool[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    shots[i, j] = false;
                }
            }
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            shots = new bool[10, 10];

            shipX = new int[100];
            shipY = new int[100];
            numOfShips = 0;
            shipIndex = 0;

            cHit = 5;
            bHit = 4;
            dHit = 3;
            sHit = 3;
            pHit = 2;
        }

        public override string GetNickname() //Used to make nickname, it will be such
        {
            string nickname = "Sergeant Simon";
            return nickname; //returns the string when method is called 
        }


        public override string SetOpponent(string opponent)
        {
            return $"Battleship Agent '{GetNickname()}'"; //gets opponents nickname
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet(); // wasn't sure if the position was supposed to be different every game, but this is what i did

            myFleet.Carrier = new ShipPosition(1, 0, ShipRotation.Horizontal); //a preset location for all my boats, statistically, the best layout
            myFleet.Battleship = new ShipPosition(5, 9, ShipRotation.Horizontal); //not sure if the ai will understand and just hit the same place
            myFleet.Destroyer = new ShipPosition(3, 6, ShipRotation.Horizontal);
            myFleet.Submarine = new ShipPosition(5, 5, ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(9, 2, ShipRotation.Vertical);
        }

        public override GridSquare LaunchAttack()
        {

            if (shipIndex < numOfShips) //
            {
                attackGrid.x = shipX[shipIndex];
                attackGrid.y = shipY[shipIndex];
                shots[attackGrid.x, attackGrid.y] = true;
                shipX = attackGrid.x;
                shipY = attackGrid.y;
                shipIndex++;
                return attackGrid;
            }

            Random rnd = new Random(); int x = 9; int y = 9;
            //my agent will randomly chose the an x value and a y value in between 0 and 9
            {
                x = rnd.Next(9); //in the array, the board is 0-9
                y = rnd.Next(9); 
            }

            attackGrid.x = x; preX = x;
            attackGrid.y = y; preY = y;
            shots[x, y] = true;
            return attackGrid;
        }

        public void UpdateNeighbors()
        { //method used for hits, runs through all if statements until it completely destroys found target
            //im not to sure how to make it go in a sigular direction once it hits twice
            //nor do i know how to make it run down a singular line in case it missed one

            if (Hit(preX, preY + 1)) 
            {
                shipX[numOfShips] = preX;
                shipY[numOfShips] = preY + 1;
                numOfShips++;
            }
            if (Hit(preX, preY - 1))
            {
                shipX[numOfShips] = preX;
                shipY[numOfShips] = preY - 1;
                numOfShips++;
            }

            if (Hit(preX + 1, preY))
            {
                shipX[numOfShips] = preX + 1;
                shipY[numOfShips] = preY;
                numOfShips++;
            }
            if (Hit(preX - 1, preY))
            {
                shipX[numOfShips] = preX - 1;
                shipY[numOfShips] = preY;
                numOfShips++;
            }

        }

        public override void DamageReport(char report)
        {

            if (report != ShipType.None)
            {

                UpdateTarget(); //used to update where ai will target next

                if (report == ShipType.Carrier)
                {
                    if (cHit == 5)
                    {

                    }
                    cHit--;

                    if (cHit == 0)
                    {
                        shipIndex = numOfShips;
                    }
                }
                if (report == ShipType.Battleship)
                {
                    if (bHit == 4)
                    {
                    }
                    bHit--;

                    if (bHit == 0)
                    {
                        shipIndex = numOfShips;
                    }
                }
                if (report == ShipType.Destroyer)
                {
                    if (dHit == 3)
                    {
                    }
                    dHit--;

                    if (dHit == 0)
                    {
                        shipIndex = numOfShips;
                    }
                }
                if (report == ShipType.Submarine)
                {
                    if (sHit == 3)
                    {
                    }
                    sHit--;

                    if (sHit == 0)
                    {
                        shipIndex = numOfShips;
                    }
                }
                if (report == ShipType.PatrolBoat)
                {
                    if (pHit == 2)
                    {
                    }
                    pHit--;

                    if (pHit == 0)
                    {
                        shipIndex = numOfShips;
                    }
                }
            }
        }
    }
}













