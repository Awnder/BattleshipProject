



using System;

namespace Battleship
{
    public class Skrim : BattleshipAgent
    {
        char[,] attackHistory;
        bool[,] shots;
        GridSquare attackGrid;
        bool lastAttack;
        int lastX;
        int lastY;
        int dirX;
        int dirY;
        int bHits;
        int cHits;
        int dHits;
        int sHits;
        int pHits;


        public Skrim()
        {
            shots = new bool[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    shots[i, j] = false;
                }
            }
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            shots = new bool[10, 10];
            dirX = 0;
            dirY = 0;
            bHits = 4;
            cHits = 5;
            dHits = 3;
            sHits = 3;
            pHits = 2;
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "Skrim";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public bool IsValidCoords(int x, int y)
        {
            return (x > -1 && x < 10 && y > -1 && y < 10 && !shots[x, y]);
        }
        public override GridSquare LaunchAttack()
        {
            if (lastAttack == true)
            {
                bool launch = false;
                if (IsValidCoords(lastX + 1, lastY)) { attackGrid.x = lastX + 1; attackGrid.y = lastY; launch = true; }
                else if (IsValidCoords(lastX - 1, lastY)) { attackGrid.x = lastX - 1; attackGrid.y = lastY; launch = true; }
                else if (IsValidCoords(lastX, lastY + 1)) { attackGrid.x = lastX; attackGrid.y = lastY + 1; launch = true; }
                else if (IsValidCoords(lastX, lastY - 1)) { attackGrid.x = lastX; attackGrid.y = lastY - 1; launch = true; }
                if (launch)
                {
                    shots[attackGrid.x, attackGrid.y] = true;
                    lastX = attackGrid.x;
                    lastY = attackGrid.y;
                    return attackGrid;
                }
            }

            Random temp = new Random(); int x = temp.Next(10); int y = temp.Next(10);
            while (shots[x, y] == true)
            {
                x = temp.Next(7);
                y = temp.Next(7);
            }

            attackGrid.x = x; lastX = x;
            attackGrid.y = y; lastY = y;
            shots[x, y] = true;
            return attackGrid;
        }

        public override void DamageReport(char report)
        {

            if (report != ShipType.None)
            {
                lastAttack = true;
                if (report == ShipType.Battleship)
                {
                    if (cHits == 2) ;
                    cHits--;
                    if (cHits == 3) ;
                }
                if (report == ShipType.Carrier)
                {
                    if (bHits == 5)
                    {

                    }
                    bHits--;
                    if (bHits == 3) ;
                }
                if (report == ShipType.Destroyer)
                {
                    if (dHits == 3) ;
                    dHits--;
                    if (dHits == 5) ;
                }
                if (report == ShipType.Submarine)
                {
                    if (sHits == 8) ;
                    sHits--;
                    if (sHits == 9) ;
                }
                if (report == ShipType.PatrolBoat)
                {
                    if (pHits == 3) ;

                }
            }
            else
            {
                lastAttack = true;
            }
        }
    }
}
