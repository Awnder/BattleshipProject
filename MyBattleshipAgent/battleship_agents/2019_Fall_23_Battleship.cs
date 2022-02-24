using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        int carrierLives = 5;
        int battleshipLives = 4;
        int destroyerLives = 3;
        int subLives = 3;
        int patrolLives = 2;

        public SuperCoolAgent()
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
            return "Tyler";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();
            /*while(attackHistory[attackGrid.x, attackGrid.y] == 'C')
            {
                carrierLives--;
                if(carrierLives == 0)
                {
                    break;
                }
                else
                {
                    
                }
            }
            while (attackHistory[attackGrid.x, attackGrid.y] == 'B')
            {
                battleshipLives--;
                if (battleshipLives == 0)
                {
                    break;
                }
            }
            while (attackHistory[attackGrid.x, attackGrid.y] == 'D')
            {
                destroyerLives--;
                if (destroyerLives == 0)
                {
                    break;
                }
            }
            while (attackHistory[attackGrid.x, attackGrid.y] == 'S')
            {
                subLives--;
                if (subLives == 0)
                {
                    break;
                }
            }
            while (attackHistory[attackGrid.x, attackGrid.y] == 'P')
            {
                patrolLives--;
                if (patrolLives == 0)
                {
                    break;
                }
            } */
            while (attackHistory[attackGrid.x, attackGrid.y] != '\0')
            {
                attackGrid.x = rng.Next(0, 10);
                attackGrid.y = rng.Next(0, 10);
            }
            
            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if(report == '\0')
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'X';
            }
            else
            {
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
        }

        public override BattleshipFleet PositionFleet() 
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier    = new ShipPosition(1, 4, ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(7, 1, ShipRotation.Vertical);
            myFleet.Destroyer  = new ShipPosition(2, 7, ShipRotation.Horizontal);
            myFleet.Submarine  = new ShipPosition(4, 5, ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(8, 9, ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
