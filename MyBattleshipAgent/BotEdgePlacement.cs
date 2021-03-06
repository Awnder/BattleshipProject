using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    public class BotEdgePlacement : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;

        public BotEdgePlacement()
        {
            Nickname = "Bot Edge Placement";
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
        }

        public override void Initialize()
        {
            //make or clear attackHistory
            for (int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for (int j = 0; j < attackHistory.GetLength(1); j++)
                {
                    attackHistory[i, j] = 'U'; //u - unknown
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
            return "Bot Edge Placement";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            attackGrid.x = 0;
            attackGrid.y = 0;
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

            myFleet.Carrier = new ShipPosition(0, 0, ShipRotation.Horizontal);
            myFleet.Battleship = new ShipPosition(0, 2, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(9, 3, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(0, 9, ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(7, 9, ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
