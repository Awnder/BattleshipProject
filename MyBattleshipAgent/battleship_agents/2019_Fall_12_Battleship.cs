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
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "RocketMan";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();

            while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None || attackHistory[attackGrid.x, attackGrid.y] == 'F')
            {
                attackGrid.x = rng.Next(10);
                attackGrid.y = rng.Next(10);
            }



            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if (report == ShipType.None)
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'x';
            }
            else
            {
                attackHistory[attackGrid.x, attackGrid.y] = report; // \0 C B D S P
            }

            int carrierLives = 5;
            int battleshipLives = 4;
            int submarineLives = 3;
            int destroyerLives = 3;
            int patrolLives = 2;

            if (report == 'C')
            {
                carrierLives--;

                if (carrierLives == 4)
                {
                    //start sweep

                    return;
                }
                

                while(carrierLives != 0)
                {
                    //direction sweep
                }

                if (carrierLives == 0)
                {
                    report = 'F';
                }
            }

            if (report == 'B')
            {
                battleshipLives--;

                if (battleshipLives == 3)
                {
                    //start sweep

                    return;
                }

                while (battleshipLives != 0)
                {
                    //sweep
                }

                if (battleshipLives == 0)
                {
                    report = 'F';
                }
            }

            if (report == 'S')
            {
                submarineLives--;

                if (submarineLives == 2)
                {
                    //start sweep

                    return;
                }

                while (submarineLives != 0)
                {
                    //sweep
                }

                if (submarineLives == 0)
                {
                    report = 'F';
                }
            }

            if (report == 'D')
            {
                destroyerLives--;

                if (destroyerLives == 2)
                {
                    //start sweep

                    return;
                }

                while (destroyerLives != 0)
                {
                    report = 'F';
                }

                if (destroyerLives == 0)
                {
                    report = 'F';
                }
            }

            if (report == 'P')
            {
                patrolLives--;
                while (patrolLives != 0)
                {
                    //sweep
                }

                if (patrolLives == 0)
                {
                    report = 'F';
                }
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            myFleet.Carrier    = new ShipPosition(0, 4, 'V');
            myFleet.Battleship = new ShipPosition(3, 9, 'H');
            myFleet.Destroyer  = new ShipPosition(9, 7, 'V');
            myFleet.Submarine  = new ShipPosition(2, 2, 'H');
            myFleet.PatrolBoat = new ShipPosition(4, 4, 'V');

            return myFleet;
        }
    }
}
