using System;

namespace Battleship
{
    public class RealMadridFan : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;


        public RealMadridFan()
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
            return "Messi";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
			Random rng = new Random();

            while(attackHistory[attackGrid.x, attackGrid.y] = ShipType.None)
			{
				attackGrid.x = rng.Next(10);
				attackGrid.y = rng.Next(10);
			}

			return attackGrid; 
        }

        public override void DamageReport(char report)
        {
            if(report== ShipType.None)
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

            myFleet.Carrier    = new ShipPosition(0, 2, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(9, 6, ShipRotation.Horizontal);
            myFleet.Destroyer  = new ShipPosition(3, 2, ShipRotation.Vertical);
            myFleet.Submarine  = new ShipPosition(5, 9, ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(4, 7, ShipRotation.Vertical);
			myFleet.PatrolBoat = new ShipPosition(rng.Next(3, 6), rng.Next(2, 5), ShipRotation.Vertical);

            return myFleet;
        }
    }
}
