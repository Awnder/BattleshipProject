using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Battleship 
{
    public class SquareSpiralAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng;
        int direction; //0-3: down, right, up, left
        int[] recordedSpace;

        public SquareSpiralAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            rng = new Random();
            direction = 0;
            recordedSpace = new int[2];
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
            return "Square Spiral Agent";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            AttackSquareSpiral();
            //AttackShip();

            AttackFailSafe();

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

            int[,] myFleetPlacements = CreateFleetPlacement(); //order: patrolboat, sub, destroyer, battleship, carrier
            bool safe = true;

            //failsafe placements
            for (int x = 0; x < myFleetPlacements.GetLength(0); x++)
            {
                for (int y = 0; y < myFleetPlacements.GetLength(1) - 1; y++)
                {
                    if (myFleetPlacements[x, y] < 0 || myFleetPlacements[x, y] > 9)
                    {
                        safe = false;
                    }
                }
            }

            if (safe == true)
            {
                if (myFleetPlacements[4, 2] == 0)
                {
                    myFleet.Carrier = new ShipPosition(myFleetPlacements[4, 0], myFleetPlacements[4, 1], ShipRotation.Horizontal);
                }
                else
                {
                    myFleet.Carrier = new ShipPosition(myFleetPlacements[4, 0], myFleetPlacements[4, 1], ShipRotation.Vertical);
                }
                if (myFleetPlacements[3, 2] == 0)
                {
                    myFleet.Battleship = new ShipPosition(myFleetPlacements[3, 0], myFleetPlacements[3, 1], ShipRotation.Horizontal);
                }
                else
                {
                    myFleet.Battleship = new ShipPosition(myFleetPlacements[3, 0], myFleetPlacements[3, 1], ShipRotation.Vertical);
                }
                if (myFleetPlacements[2, 2] == 0)
                {
                    myFleet.Destroyer = new ShipPosition(myFleetPlacements[2, 0], myFleetPlacements[2, 1], ShipRotation.Horizontal);
                }
                else
                {
                    myFleet.Destroyer = new ShipPosition(myFleetPlacements[2, 0], myFleetPlacements[2, 1], ShipRotation.Vertical);
                }
                if (myFleetPlacements[1, 2] == 0)
                {
                    myFleet.Submarine = new ShipPosition(myFleetPlacements[1, 0], myFleetPlacements[1, 1], ShipRotation.Horizontal);
                }
                else
                {
                    myFleet.Submarine = new ShipPosition(myFleetPlacements[1, 0], myFleetPlacements[1, 1], ShipRotation.Vertical);
                }
                if (myFleetPlacements[0, 2] == 0)
                {
                    myFleet.PatrolBoat = new ShipPosition(myFleetPlacements[0, 0], myFleetPlacements[0, 1], ShipRotation.Horizontal);
                }
                else
                {
                    myFleet.PatrolBoat = new ShipPosition(myFleetPlacements[0, 0], myFleetPlacements[0, 1], ShipRotation.Vertical);
                }
            }
            else
            {
                myFleet.Carrier = new ShipPosition(3, 1, ShipRotation.Horizontal);
                myFleet.Battleship = new ShipPosition(3, 3, ShipRotation.Horizontal);
                myFleet.Destroyer = new ShipPosition(3, 5, ShipRotation.Horizontal);
                myFleet.Submarine = new ShipPosition(3, 7, ShipRotation.Horizontal);
                myFleet.PatrolBoat = new ShipPosition(3, 9, ShipRotation.Horizontal);
            }

            return myFleet;
        }

        // --- METHODS TO PLACE FLEET ---

        private int[,] CreateFleetPlacement()
        {
            char[,] board = new char[10, 10];
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 'U'; //u - unknown
                }
            }

            board = SetShipPlacement(board);

            int[,] fleetPlacements = new int[5, 3]; //five ships, three items (x,y,horiz/vert)
            for (int q = 0; q < 5; q++) //for five ships
            {
                int[] tempPlacements = GetShipPlacement(board, q);
                for (int r = 0; r < 3; r++) //for three items
                {
                    fleetPlacements[q, r] = tempPlacements[r];
                }
            }
            return fleetPlacements;
        }

        private char[,] SetShipPlacement(char[,] board, int shipNumber = 0)
        {
            for (int i = shipNumber; i < 5; i++)
            {
                int[] placement = GetXYPlacement(i);
                if (placement[2] == 0) //horizontal
                {
                    int counter = 0;
                    for (int x = placement[0]; x < placement[0] + GetShipLength(i); x++)
                    {
                        if (board[x, placement[1]] == 'U')
                        {
                            counter++;
                        }
                        else
                        {
                            board = SetShipPlacement(board, i);
                            return board;
                        }
                    }
                    if (counter == GetShipLength(i))
                    {
                        for (int x = placement[0]; x < placement[0] + GetShipLength(i); x++)
                        {
                            board[x, placement[1]] = GetShipLetter(i);
                        }
                    }
                }
                else //vertical
                {
                    int counter = 0;
                    for (int y = placement[1]; y < placement[1] + GetShipLength(i); y++)
                    {
                        if (board[placement[0], y] == 'U')
                        {
                            counter++;
                        }
                        else
                        {
                            board = SetShipPlacement(board, i);
                            return board;
                        }
                    }
                    if (counter == GetShipLength(i))
                    {
                        for (int y = placement[1]; y < placement[1] + GetShipLength(i); y++)
                        {
                            board[placement[0], y] = GetShipLetter(i);
                        }
                    }
                }
            }
            return board;
        }

        private int[] GetShipPlacement(char[,] board, int ship)
        {
            int[] shipRecord = new int[] { 0, 0, 0 };
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] == GetShipLetter(ship))
                    {
                        shipRecord[0] = x;
                        shipRecord[1] = y;
                        //make sure x+1 and y+1 are in bounds
                        if (x == 9)
                        {
                            shipRecord[2] = 1;
                        }
                        else
                        {
                            if (board[x + 1, y] == GetShipLetter(ship))
                            {
                                shipRecord[2] = 0; //horizontal
                            }
                        }
                        if (y == 9)
                        {
                            shipRecord[2] = 0;
                        }
                        else
                        {
                            if (board[x, y + 1] == GetShipLetter(ship))
                            {
                                shipRecord[2] = 1; //vertical
                            }
                        }
                        return shipRecord;
                    }
                }
            }
            return shipRecord;
        }

        private int[] GetXYPlacement(int i)
        {
            int[] placement = new int[3]; //x, y, horiz/vert
            int shipPlacement = rng.Next(2); //0 horiz, 1 vert
            if (shipPlacement == 0)
            {
                placement[0] = rng.Next(10 - GetShipLength(i));
                placement[1] = rng.Next(10);
            }
            else
            {
                placement[0] = rng.Next(10);
                placement[1] = rng.Next(10 - GetShipLength(i));
            }
            placement[2] = shipPlacement;
            return placement;
        }

        private char GetShipLetter(int i)
        {
            char carrier = 'C';
            char battleship = 'B';
            char destroyer = 'D';
            char submarine = 'S';
            char patrolboat = 'P';

            switch (i)
            {
                case 4:
                    return carrier;
                case 3:
                    return battleship;
                case 2:
                    return destroyer;
                case 1:
                    return submarine;
                case 0:
                    return patrolboat;
                default:
                    return 'X';
            }
        }

        private int GetShipLength(int i)
        {
            int carrierLength = 5;
            int battleshipLength = 4;
            int destroyerLength = 3;
            int submarineLength = 3;
            int patrolboatLength = 2;

            switch (i)
            {
                case 4:
                    return carrierLength;
                case 3:
                    return battleshipLength;
                case 2:
                    return destroyerLength;
                case 1:
                    return submarineLength;
                case 0:
                    return patrolboatLength;
                default:
                    return 1;
            }
        }

        // --- METHODS TO PLACE ATTACKS ---

        private bool IsShip(int x, int y)
        {
            if (attackHistory[x, y] == 'C' || attackHistory[x, y] == 'B' || attackHistory[x, y] == 'D' || attackHistory[x, y] == 'S' || attackHistory[x, y] == 'P')
            {
                return true;
            }
            return false;
        }

        private bool IsUnknown(int x, int y)
        {
            if (attackHistory[x, y] == 'U')
            {
                return true;
            }
            return false;
        }

        private void SetAttack(int x, int y)
        {
            if (IsUnknown(x, y))
            {
                attackGrid.x = x;
                attackGrid.y = y;
            }
        }

        private void RecordSpace(int x, int y)
        {
            recordedSpace[0] = x;
            recordedSpace[1] = y;
        }

        private void AttackInit()
        {
            SetAttack(4, 4);
            RecordSpace(4, 4);
        }
        
        private void AttackSquareSpiral()
        {
            if (direction == 0)
            {
                for (int y = recordedSpace[1]; y < recordedSpace[1] + GetSpace(); y++)
                {
                    SetAttack(recordedSpace[0], y);
                }
            } else
            {

            }

        }

        private int GetSpace()
        {
            int[] array = ConvertSpaceToInt().ToArray();
            return array[array.Length-1];
        }

        private IEnumerable<int> ConvertSpaceToInt()
        {
            List<int> spaces = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 9 };
            for (int i = 0; i < spaces.Count; i++)
            {
                yield return spaces[i];
            }
        }
    
        private void AttackShip()
        {
            for (int x = 0; x < attackHistory.GetLength(0); x++)
            {
                for (int y = 0; y < attackHistory.GetLength(1); y++)
                {
                    if (IsShip(x, y))
                    {
                        AttackCheckUp(x, y);
                        AttackCheckDown(x, y);
                        AttackCheckLeft(x, y);
                        AttackCheckRight(x, y);
                    }
                }
            }
        }

        private void AttackCheckUp(int x, int y)
        {
            if (y == 0)
            {
                return;
            }
            if (attackHistory[x, y - 1] == 'M')
            {
                return;
            }
            if (attackHistory[x, y - 1] == 'U')
            {
                SetAttack(x, y - 1);
            }
        }

        private void AttackCheckDown(int x, int y)
        {
            if (y == 9)
            {
                return;
            }
            if (attackHistory[x, y + 1] == 'M')
            {
                return;
            }
            if (attackHistory[x, y + 1] == 'U')
            {
                SetAttack(x, y + 1);
            }
        }

        private void AttackCheckLeft(int x, int y)
        {
            if (x == 0)
            {
                return;
            }
            if (attackHistory[x - 1, y] == 'M')
            {
                return;
            }
            if (attackHistory[x - 1, y] == 'U')
            {
                SetAttack(x - 1, y);
            }
        }

        private void AttackCheckRight(int x, int y)
        {
            if (x == 9)
            {
                return;
            }
            if (attackHistory[x + 1, y] == 'M')
            {
                return;
            }
            if (attackHistory[x + 1, y] == 'U')
            {
                SetAttack(x + 1, y);
            }
        }

        private void AttackFailSafe()
        {
            if (attackGrid.x < 0 || attackGrid.x > 9 || attackGrid.y < 0 || attackGrid.y > 9)
            {
                do
                {
                    SetAttack(rng.Next() % 10, rng.Next() % 10);
                } while (attackHistory[attackGrid.x, attackGrid.y] != 'U');
            }
        }
    }
}
