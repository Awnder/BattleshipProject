using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        Random rng;

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            for(int i = 0; i < attackHistory.GetLength(0); i++)
            {
                for(int j = 0; j < attackHistory.GetLength(1); j++)
                {
                    attackHistory[i, j] = 'U'; //u - unknown
                }
            }
            attackGrid = new GridSquare();
            rng = new Random();
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
            return "Andrew's Bot";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            attackGrid.x = 5;
            attackGrid.y = 5;
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if(report == '\0')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'M';
            } else
            {
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            CreateFleetPlacement();

            myFleet.Carrier = new ShipPosition(0, 0, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(1, 0, ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(2, 0, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(3, 0, ShipRotation.Vertical);
            myFleet.PatrolBoat = new ShipPosition(4, 0, ShipRotation.Horizontal);

            return myFleet;
        }

        private void CreateFleetPlacement()
        {
            char[,] board = new char[10, 10];
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = 'U'; //u - unknown
                }
            }
            board = SetShipPlacement(board);

            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
        }

        private char[,] SetShipPlacement(char[,] board, int ship = 0)
        {
            for(int i = ship; i < 5; i++)
            {
                int[] placement = GetXYPlacement(i);
                if(placement[2] == 0)
                {
                    for(int a = placement[0]; a < GetShipLength(i); a++)
                    {
                        for(int b = placement[1]; b < GetShipLength(i); b++)
                        {
                            if(board[a, b] == 'U')
                            {
                                board[a, b] = GetShipLetter(i);
                            }
                            else
                            {
                                SetShipPlacement(board, i);
                            }
                        }
                    }
                } else
                {
                    for(int a = placement[0]; a < GetShipLength(i); a++)
                    {
                        for(int b = placement[1]; b < GetShipLength(i); b++)
                        {
                            if(board[b, a] == 'U')
                            {
                                board[b, a] = GetShipLetter(i);
                            } else
                            {
                                SetShipPlacement(board, i);
                            }
                        }
                    }
                }
                
            }
            return board;
        }

        private int[] GetXYPlacement(int x)
        {
            int[] placement = new int[3];
            int shipPlacement = rng.Next(2); //0 horiz, 1 vert
            if(shipPlacement == 0)
            {
                placement[0] = rng.Next(10 - GetShipLength(x));
                placement[1] = rng.Next(10);
            }
            else
            {
                placement[0] = rng.Next(10);
                placement[1] = rng.Next(10 - GetShipLength(x));
            }
            placement[2] = shipPlacement;
            return placement;
        }

        private char GetShipLetter(int x)
        {
            char carrier = 'C';
            char battleship = 'B';
            char destroyer = 'D';
            char submarine = 'S';
            char patrolboat = 'P';

            switch(x)
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

        private int GetShipLength(int x)
        {
            int carrierLength = 5;
            int battleshipLength = 4;
            int destroyerLength = 3;
            int submarineLength = 3;
            int patrolboatLength = 2;

            switch (x)
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
                    return -1;
            }
        }
    }
}
