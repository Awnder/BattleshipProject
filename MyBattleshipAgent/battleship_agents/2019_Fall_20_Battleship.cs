using System;
using System.Collections.Generic;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        List<GridSquare> hitList;

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            hitList = new List<GridSquare>();

        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
            
        }

        public override string GetNickname()
        {
            return "~ Nightwing ~";
        }

        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();

            if(hitList.Count > 0)
            {
                GridSquare temp = hitList[0];

                if (temp.y - 1 >= 0 && attackHistory[temp.x, temp.y - 1] == ShipType.None)
                {
                    attackGrid.x = temp.x;
                    attackGrid.y = temp.y - 1;
                }
                else if (temp.y + 1 <= 9 && attackHistory[temp.x, temp.y + 1] == ShipType.None)
                {
                    attackGrid.x = temp.x;
                    attackGrid.y = temp.y + 1;
                }
                else if (temp.x + 1 <= 9 && attackHistory[temp.x + 1, temp.y] == ShipType.None)
                {
                    attackGrid.x = temp.x + 1;
                    attackGrid.y = temp.y;
                }
                else if (temp.x - 1 >= 0 && attackHistory[temp.x - 1, temp.y] == ShipType.None)
                {
                    attackGrid.x = temp.x - 1;
                    attackGrid.y = temp.y;
                }
                else
                {
                    hitList.RemoveAt(0);
                }
            }

            else
            {
                while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
                {
                    attackGrid.x = rng.Next(10);
                    attackGrid.y = rng.Next(10);
                }
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

                hitList.Add(attackGrid);
            }
        }
                                    

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            Random rng = new Random();

            rng.Next(0, 10);



            myFleet.Carrier    = new ShipPosition(0, 5 , 'H');
            myFleet.Battleship = new ShipPosition(6, 7, 'H');
            myFleet.Destroyer  = new ShipPosition(2, 1, 'V');
            myFleet.Submarine  = new ShipPosition(9, 0, 'V');
            myFleet.PatrolBoat = new ShipPosition(7, 2, 'V');

            return myFleet;
        }
    }
}
