using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Battleship
{
    public class HeiHei : BattleshipAgent
	{
		char[,] attackHistory;
		GridSquare attackGrid;
		List<GridSquare> nextShots;

		public HeiHei()
		{
			attackHistory = new char[10, 10];
			attackGrid = new GridSquare();
			nextShots = new List<GridSquare>();
		}
		public override string ToString()
		{
			return $"Battleship Agent '{GetNickname()}'";
		}
		public override GetNickname()
		{
			return "Koto";
		}
		public override void SetOpponent(string opponent)
		{
			return;
		}

		public override GridSquare LaunchAttack()
		{
			Random rdm = new Random();
			if (nextShots.Count > 0)
			{
				GridSquare hitDirection = nextShots[0];
				if (hitDirection.y - 1 >= 0 && attackHistory[hitDirection.x, hitDirection.y - 1] == ShipType.None)
				{
					attackGrid.x = hitDirection.x;
					attackGrid.y = hitDirection.y - 1;
				}
				else if (hitDirection.y + 1 <= 9 && attackHistory[hitDirection.x, hitDirection.y + 1] == ShipType.None)
				{
					attackGrid.x = hitDirection.x;
					attackGrid.y = hitDirection.y + 1;
				}
				else if (hitDirection.x + 1 <= 9 && attackHistory[hitDirection.x + 1, hitDirection.y] == ShipType.None)
				{
					attackGrid.x = hitDirection.x + 1;
					attackGrid.y = hitDirection.y;
				}
				else if (hitDirection.x + 1 >= 0 && attackHistory[hitDirection.x - 1, hitDirection.y] == ShipType.None)
				{
					attackGrid.x = hitDirection.x - 1;
					attackGrid.y = hitDirection.y;
				}
				else
				{
					nextShots.RemoveAt(0);
				}
			}
			else
			{
				while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
				{
					attackGrid.x = rdm.Next(10);
					attackGrid.y = rdm.Next(10);
				}
			}
			return attackGrid;
		}
		public override void DamageReport(char report)
		{
			if (report == ShipType.None)
			{
				attackHistory[attackGrid.x, attackGrid.y] = "X";
			}
			else
			{
				attackHistory[attackGrid.x, attackGrid.y] = report;
				nextShots.Add(attackGrid);
			}
		}
		public override BattleShipFleet Position()
		{
			BattleshipFleet mySquad = new BattleshipFleet();
			Random rdm = new Random();

			rdm.Next(0, 10);

			mySquad.Carrier = new ShipPosition(7, 5, ShipRotation.Vertical);
			mySquad.Battleship = new ShipPosition(2, 8, ShipRotation.Horizontal);
			mySquad.Destroyer = new ShipPosition(9, 1, ShipRotation.Vertical);
			mySquad.Submarine = new ShipPosition(3, 4, ShipRotation.Horizontal);
			mySquad.PatrolBoat = new ShipPosition(0, 6, ShipRotation.Vertical);

		}

		//Everything below is a reference code from stackoverflow
  
	}
	//public void NewGame(Size size, TimeSpan timeSpan)
	//{
	//	gameSize = size;
	//	scanShots = new List<Point>();
	//	nextShots = new List<NextShot>();
	//	fillScanShots();
	//	hitDirection = Direction.Unknown;
	//	board = new ShotResult[size.Width, size.Height];
	//}

	//private void fillScanShots()
	//{
	//	int x;
	//	for (x = 0; x < gameSize.Width - 1; x++)
	//	{
	//		scanShots.Add(new Point(x, x));
	//	}

	//	if (gameSize.Width == 10)
	//	{
	//		for (x = 0; x < 3; x++)
	//		{
	//			scanShots.Add(new Point(9 - x, x));
	//			scanShots.Add(new Point(x, 9 - x));
	//		}
	//	}
	//}
	//}
	//public Point GetShot()
	//{
	//	Point shot;
	//
	//	if (this.nextShots.Count > 0)
	//	{
	//		if (hitDirection != Direction.UNKNOWN)
	//		{
	//			if (hitDirection == Direction.HORIZONTAL)
	//			{
	//				this.nextShots = this.nextShots.OrderByDescending(x => x.direction).ToList();
	//			}
	//			else
	//			{
	//				this.nextShots = this.nextShots.OrderBy(x => x.direction).ToList();
	//			}
	//		}
	//		shot = this.nextShots.First().point;
	//		lastShotDirection = this.nextShots.First().direction;
	//		this.nextShots.RemoveAt(0);
	//		return shot;
	//	}

	//List<ScanShot> scanShots = new List<ScanShot>();
	//	for (int x = 0; x < gameSize.Width; x++)
	//	{
	//		for (int y = 0; y < gameSize.Height; y++)
	//		{
	//			if (board[x, y] == ShotResult.UNKNOWN)
	//			{
	//				scanShots.Add(new ScanShot(new Point(x, y), OpenSpaces(x, y)));
	//			}
	//		}
	//	}
	//	scanShots = scanShots.OrderByDescending(x => x.openSpaces).ToList();
	//	int maxOpenSpaces = scanShots.FirstOrDefault().openSpaces;
	//	List<ScanShot> scanShots2 = new List<ScanShot>();
	//	scanShots2 = scanShots.Where(x => x.openSpaces == maxOpenSpaces).ToList();
	//	shot = scanShots2[rand.Next(scanShots2.Count())].point;
	//	return shot;

}
