using System;
using System.Collections.Generic;
namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        List<GridSquare> record;
        GridSquare attPlane;
        char[,] pastAtt;
        public SuperCoolAgent()
        {
            attPlane = new GridSquare();
            record = new List<GridSquare>();
            pastAtt = new char[10, 10];
        }
        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }
        public override string GetNickname()
        {
            return "DatBoi";
        }
        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();
            if(record.Count > 0)
            {
                GridSquare temp = record[0];
                if (temp.y-1 >= 0 && pastAtt[temp.x, temp.y-1] == ShipType.None)
                {attPlane.x = temp.x;
                 attPlane.y = temp.y-1;}
                else if (temp.y+1 < 10 && pastAtt[temp.x, temp.y+1] == ShipType.None)   
                {attPlane.x = temp.x;
                 attPlane.y = temp.y-1;}
                else if (temp.x+1 <= 0 && pastAtt[temp.x+1, temp.y] == ShipType.None) 
                {attPlane.x = temp.x+1;
                 attPlane.y = temp.y;}
                else if (temp.x-1 >= 0 && pastAtt[temp.x-1, temp.y] == ShipType.None) 
                {attPlane.x = temp.x-1;
                 attPlane.y = temp.y;}
                else
                {record.RemoveAt(0);}
            }
            else
            {
                while (pastAtt[attPlane.x, attPlane.y] != ShipType.None)
                {attPlane.x = rng.Next(10);
                 attPlane.y = rng.Next(10);}
            }
            return attPlane;
        }
        public override void DamageReport(char report)
        {
            if (report == ShipType.None)
            {
                pastAtt[attPlane.x, attPlane.y] = 'X';   
            }
            else
            {
                pastAtt[attPlane.x, attPlane.y] = report;
                record.Add(attPlane);
            }
        }
        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();
            rng.Next(0, 10);
            myFleet.Carrier    = new ShipPosition(1, 5, 'V');
            myFleet.Battleship = new ShipPosition(2, 3, 'V');
            myFleet.Destroyer  = new ShipPosition(4, 1, 'V');
            myFleet.Submarine  = new ShipPosition(7, 0, 'V');
            myFleet.PatrolBoat = new ShipPosition(9, 4, 'V');
            return myFleet;
        }
    }
}
