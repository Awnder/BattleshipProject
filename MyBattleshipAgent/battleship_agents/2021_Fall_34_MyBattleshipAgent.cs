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
            for (int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for (int j = 0; j < attackHistory.GetLength(0); j++)
                {
                    attackHistory[i, j] = 'U';
                }

            }
            attackGrid = new GridSquare();
            attackGrid.y = 0;
            attackGrid.x = -1;
        }

        public override void Initialize()
        {
            return;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "The Destroyer";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
                 if (attackGrid.y % 2 == 0)
                 {
                    if (attackGrid.x < 8)
                    {
                        attackGrid.x += 1;
                    }
                    else
                    {
                        attackGrid.x = 1;
                        attackGrid.y += 1;
                    }
                 }
                else
                {
                    if (attackGrid.x < 8)
                    {
                        attackGrid.x += 1;
                    }
                    else
                    {
                        attackGrid.x = 1;
                        attackGrid.y += 1;
                    }
                }



            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if (report == '\0')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'X';
            }
            else if(report == 'C' || report == 'B' || report == 'D' || report =='S' || report == 'P')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'H';
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
            myFleet.Battleship = new ShipPosition(1, 0, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(2, 0, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(3, 0, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(4, 0, ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
