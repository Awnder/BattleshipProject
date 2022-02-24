using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Agent Smith";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            int[] evens = { 0, 2, 4, 6, 8 };
            int[] odds = { 1, 3, 5, 7, 9 };
            int BenchmarkX = attackGrid.x;
            int BenchmarkY = attackGrid.y;
            Random rng = new Random();
            int modeToggle = 0;


            if (attackHistory[attackGrid.x, attackGrid.y] != 'X') //if a ship is hit, change to Sweep mode
            {
                modeToggle = 1;
            }

            //Hunt mode
                while ((attackHistory[attackGrid.x, attackGrid.y] != ShipType.None) & (modeToggle == 0))
            {
                
                attackGrid.x = rng.Next(0, 10);
                attackGrid.y = rng.Next(0, 10);
                //Below was a setup to only hit Odd numbers, had I completed the sweeper bugfixing I would have implimented a checkboard hit randomness.

                //if (attackGrid.x % 2 == 0) //if the x coordinate is even
                //{
                //    attackGrid.y = odds[rng.Next(0, 5)]; //shoot a random odd number between the range of 1-9
                //}
                //else //if the x coordinate is odd
                //{
                //    attackGrid.y = evens[rng.Next(0, 5)]; //shoot a random even number between the range of 0-8
                //}

            }

            //Sweep mode (never finished, as of now it only hits the target to the right of the previous stored value)
            if ((attackHistory[attackGrid.x, attackGrid.y] != ShipType.None) & (modeToggle == 1))
            {
                attackGrid.x = BenchmarkX - 1;
                attackGrid.y = BenchmarkY;
            }

            
            //sanity check for out of bound shots
            while ((attackGrid.x == -1) || (attackGrid.x == 10) || (attackGrid.y == -1) || (attackGrid.y == 10))
            {
                
                    attackGrid.x = rng.Next(0, 10);
                attackGrid.y = rng.Next(0, 10);
                
            }
            return attackGrid;
        }

        public override void DamageReport(char report)
        {


            if (report == ShipType.None)
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'X';
            }

            else
            { 
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            Random rng = new Random();
            BattleshipFleet myFleet = new BattleshipFleet();
            myFleet.Carrier = new ShipPosition(0, rng.Next(0, 4), ShipRotation.Horizontal); //Will only randomy place in top left side
            myFleet.Battleship = new ShipPosition(0, rng.Next(5, 8), ShipRotation.Horizontal); //Will only randomly place in bottom left side
            myFleet.Destroyer = new ShipPosition(rng.Next(5, 9), rng.Next(0, 3), ShipRotation.Vertical); //will only randomly place in top right side
            myFleet.Submarine = new ShipPosition(rng.Next(5, 9), rng.Next(4, 7), ShipRotation.Vertical); //will randomly place in bottom right side
            myFleet.PatrolBoat = new ShipPosition(rng.Next(0, 9), 9, ShipRotation.Horizontal); //will only randomly place in the bottom row beteween spots 0-8

            return myFleet;


        }
    }
}
