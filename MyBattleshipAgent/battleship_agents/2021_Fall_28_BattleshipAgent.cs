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

        public override void Initialize()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Christian";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }



        int REP = 0;

        

        public override GridSquare LaunchAttack()
        {
                Random rng = new Random();
                int row = rng.Next(0, 10);

                Random rng2 = new Random();
                int col = rng2.Next(0, 10);

            //search mode
            if (REP == 0)
            {
                attackGrid.x = row;
                attackGrid.y = col;
            }
            //Attack Mode 
            if (REP == 1)
            {

                while(attackGrid.x < 9)
                {
                    attackGrid.x++;
                    return attackGrid;
                }
                REP = 2;
                attackGrid.x = row;
                return attackGrid;
                
            }
            if (REP == 2)
            {

                while (attackGrid.y < 9)
                {
                    attackGrid.y++;
                    return attackGrid;
                }
                REP = 0;
                attackGrid.y = col;
                return attackGrid;
            }


            return attackGrid;

        }
        public override void DamageReport(char report)
        {
            attackHistory[attackGrid.x, attackGrid.y] = report;
            if (report != '\0')
            {
                REP = 1;
            }
            else
            {
                REP = 0;
            }
            
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier = new ShipPosition(9, 3, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(1, 1, ShipRotation.Horizontal);
            myFleet.Destroyer = new ShipPosition(2, 9, ShipRotation.Horizontal);
            myFleet.Submarine = new ShipPosition(6, 9, ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(3, 4, ShipRotation.Vertical);

            return myFleet;
        }
    }
}
