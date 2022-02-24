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
            return $"Battleship Agent '{ZMachine()}'";
        }

        public override string ZMachine()
        {
            return "ZeroCool";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
			Random rng = new Random();

            while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
			{
				attackGrid.x = rng.Next(10);
				attackGrid.y = rng.Next(10);
			}
            
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if(report == ShipType.None)
			{
				attackHistory[attackGrid.x, attackGrid.y] = 'x';
			}
			else
			{
				attackHistory[attackGrid.x, attackGrid.y] = report; // \0 C B D S P
			}
            
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();
			Random rng = new Random();

			myFleet.Carrier = new ShipPosition(rng.Next(1, 5), rng.Next(6, 2), ShipRotation.Horizonta);             //(0, 0, ShipRotation.Vertical);
			myFleet.Battleship = new ShipPosition(rng.Next(5, 2), rng.Next(1, 4), ShipRotation.Horizontal);           //1, 0, ShipRotation.Vertical);
			myFleet.Destroyer = new ShipPosition(rng.Next(2, 1), rng.Next(4, 5), ShipRotation.Vertical);             //2, 0, ShipRotation.Vertical);
			myFleet.Submarine = new ShipPosition(rng.Next(1, 3), rng.Next(2, 4), ShipRotation.Horizontal);       //3, 0, ShipRotation.Vertical);
			myFleet.PatrolBoat = new ShipPosition(rng.Next(3, 6), rng.Next(2, 5), ShipRotation.Vertical);

            return myFleet;
        }
    }
}
