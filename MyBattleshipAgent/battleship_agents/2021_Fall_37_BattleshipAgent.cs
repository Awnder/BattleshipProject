/*
 * My Battleship Agent
 * This is a program that can play a game of battleship
 * 
 * File: LEOBotBattleshipAgent.cs
 * Author: Lauren Olson
 */


using System;

namespace Battleship
{
    public class LEOBot : BattleshipAgent
    {
        char[,] ShotHistory;
        GridSquare MainGrid;
        Random rng;

        public LEOBot()
        {
            ShotHistory = new char[10, 10];
            for (int i = 0; i < ShotHistory.GetLength(0); i++)
            {
                for (int j = 0; j < ShotHistory.GetLength(0); j++)
                {
                    ShotHistory[i, j] = 'U';
                }
            }

            MainGrid = new GridSquare();
            rng = new Random();
            MainGrid.x = rng.Next(1, 11);
            MainGrid.y = rng.Next(1, 11);
        }

        public override void Initialize()
        {
            for (int i = 0; i < ShotHistory.GetLength(0); i++)
            {
                for (int j = 0; j < ShotHistory.GetLength(0); j++)
                {
                    ShotHistory[i, j] = 'U';
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
            return "LEOBot";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
                MainGrid.x = rng.Next(10);
                MainGrid.y = rng.Next(10);
            return MainGrid;
        }

        public override void DamageReport(char report)
        {
            if (report == '\0')
            {
                ShotHistory[MainGrid.x, MainGrid.y] = 'M';
            }
            else
            {
                ShotHistory[MainGrid.x, MainGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet LaurensFleet = new BattleshipFleet();

            if (rng.Next() % 2 == 1)
            {
                LaurensFleet.Destroyer = new ShipPosition(3, 1, ShipRotation.Vertical);
                LaurensFleet.Carrier = new ShipPosition(5, 8, ShipRotation.Horizontal);
                LaurensFleet.Submarine = new ShipPosition(4, 5, ShipRotation.Horizontal);
                LaurensFleet.Battleship = new ShipPosition(1, 5, ShipRotation.Vertical);
                LaurensFleet.PatrolBoat = new ShipPosition(7, 2, ShipRotation.Vertical);
            }
            else
            {
                LaurensFleet.Destroyer = new ShipPosition(3, 2, ShipRotation.Horizontal);
                LaurensFleet.Carrier = new ShipPosition(3, 7, ShipRotation.Horizontal);
                LaurensFleet.Submarine = new ShipPosition(1, 3, ShipRotation.Vertical);
                LaurensFleet.Battleship = new ShipPosition(6, 5, ShipRotation.Horizontal);
                LaurensFleet.PatrolBoat = new ShipPosition(7, 1, ShipRotation.Horizontal);

            }

            return LaurensFleet;

        }
    }
}