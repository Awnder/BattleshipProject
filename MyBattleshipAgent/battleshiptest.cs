using System;

namespace BattleshipTest
{
    class Battleship
    {
        static Random rng = new Random();

        static void Main()
        {
            CreateFleetPlacement();
        }

        private static void CreateFleetPlacement()
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

            int[,] fleetPlacements = new int[5,3]; //five ships, three items (x,y,horiz/vert)
            for(int q = 0; q < 5; q++) //for five ships
            {
                int[] tempPlacements = GetShipPlacement(board, q);
                for(int r = 0; r < 3; r++) //for three items
                {
                    fleetPlacements[q,r] = tempPlacements[r];
                }
            }

            //displaying board of ships
            Console.WriteLine();
            for(int a = 0; a < fleetPlacements.GetLength(0); a++)
            {
                for(int b = 0; b < fleetPlacements.GetLength(1); b++)
                {
                    Console.Write(fleetPlacements[a,b] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            for(int i = 0; i < board.GetLength(0); i++)
            {
                for(int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static char[,] SetShipPlacement(char[,] board, int shipNumber = 0)
        {
            for(int i = shipNumber; i < 5; i++)
            {
                Console.WriteLine($"i is : {i}");
                int[] placement = GetXYPlacement(i);
                if(placement[2] == 0) //horizontal
                {
                    int counter = 0;
                    for(int x = placement[0]; x < placement[0] + GetShipLength(i); x++)
                    {
                        if(board[x, placement[1]] == 'U')
                        {

                            counter++;
                        } else 
                        {
                            board = SetShipPlacement(board, i);
                            return board;
                        }
                    }
                    if(counter == GetShipLength(i))
                    {
                        for(int x = placement[0]; x < placement[0] + GetShipLength(i); x++)
                        {                                    
                            board[x, placement[1]] = GetShipLetter(i);
                        }
                    }
                } else //vertical
                {
                    int counter = 0;
                    for(int y = placement[1]; y < placement[1] + GetShipLength(i); y++)
                    {
                        if(board[placement[0], y] == 'U')
                        {
                            counter++;
                        } else
                        {
                            board = SetShipPlacement(board, i);
                            return board;
                        }
                    }
                    if(counter == GetShipLength(i))
                    {
                        for(int y = placement[1]; y < placement[1] + GetShipLength(i); y++)
                        {
                            board[placement[0], y] = GetShipLetter(i);
                        }
                    }
                }
            }
            return board;
        }

        private static int[] GetShipPlacement(char[,] board, int ship)
        {
            int[] shipRecord = new int[] {0,0,0};
            for(int x = 0; x < board.GetLength(0); x++)
            {
                for(int y = 0; y < board.GetLength(1); y++)
                {
                    if(board[x,y] == GetShipLetter(ship))
                    {
                        shipRecord[0] = x;
                        shipRecord[1] = y;
                        //make sure x+1 and y+1 are in bounds
                        if(x == 9)
                        {
                            shipRecord[2] = 1;
                        } else {
                            if(board[x+1,y] == GetShipLetter(ship))
                            {
                                shipRecord[2] = 0; //horizontal
                            }
                        }
                        if(y == 9)
                        {
                            shipRecord[2] = 0;
                        } else {
                            if(board[x,y+1] == GetShipLetter(ship))
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

        private static int[] GetXYPlacement(int i)
        {
            int[] placement = new int[3]; //x, y, horiz/vert
            int shipPlacement = rng.Next(2); //0 horiz, 1 vert
            if(shipPlacement == 0)
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
            Console.WriteLine($"{placement[0]},{placement[1]},{placement[2]}");
            return placement;
        }

        private static char GetShipLetter(int i)
        {
            char carrier = 'C';
            char battleship = 'B';
            char destroyer = 'D';
            char submarine = 'S';
            char patrolboat = 'P';

            switch(i)
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

        private static int GetShipLength(int i)
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
                    return -1;
            }
        }
    }
}