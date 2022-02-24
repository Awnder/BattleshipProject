using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackGrid;
        GridSquare firstHitGrid;
        char direction = 'X'; // N, S, E, W
        int battleshipLives = 4;
        int submarineLives = 3;
        int carrierLives = 5;
        int destroyerLives = 3;
        int patrolLives = 2;

        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackGrid = new GridSquare();
            firstHitGrid = new GridSquare();
        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "AgentSuperStar";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }
        public override GridSquare LaunchAttack()
        {
            Random rng = new Random();

            if (direction == 'N' && attackGrid.y >= 1)
            {
                attackGrid.x += 0;
                attackGrid.y -= 1;
            }

            else if (direction == 'S' && attackGrid.y <= 8)
            {
                attackGrid.x += 0;
                attackGrid.y += 1;
            }

            else if (direction == 'E' && attackGrid.x <= 8)
            {
                attackGrid.x += 1;
                attackGrid.y += 0;
            }

            else if (direction == 'W' && attackGrid.x >= 1)
            {
                attackGrid.x -= 1;
                attackGrid.y += 0;
            }

            else 
            {
                while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
                {
                    attackGrid.x = rng.Next(0, 10);
                    attackGrid.y = rng.Next(0, 10);
                }
            }

            return attackGrid;
        }

        public override void DamageReport(char report)
        {
            if (report == ShipType.None)
            {
                attackHistory[attackGrid.x, attackGrid.y] = 'X';

                if (direction != 'X' && firstHitGrid.y + 1 <= 9 && attackHistory[firstHitGrid.x, firstHitGrid.y + 1] == ShipType.None)
                {
                    direction = 'S';
                    attackGrid.x = firstHitGrid.x;
                    attackGrid.y = firstHitGrid.y;
                }

                else if (direction != 'X' && firstHitGrid.x + 1 <= 9 && attackHistory[firstHitGrid.x + 1, firstHitGrid.y] == ShipType.None)
                {
                    direction = 'E';
                    attackGrid.x = firstHitGrid.x;
                    attackGrid.y = firstHitGrid.y;
                }

                else if (direction != 'X' && firstHitGrid.x - 1 <= 9 && attackHistory[firstHitGrid.x - 1, firstHitGrid.y] == ShipType.None)
                {
                    direction = 'W';
                    attackGrid.x = firstHitGrid.x;
                    attackGrid.y = firstHitGrid.y; // similar to the last chunk of code, I switched it to be x + 1 since  E and W are on the x plane. Is this right?
                }
            }

            else
            {
                attackHistory[attackGrid.x, attackGrid.y] = report;

              if (report == 'B')
              {
                if (battleshipLives == 4)
                {
                   firstHitGrid.x = attackGrid.x;
                   firstHitGrid.y = attackGrid.y;
                   direction = 'N';
                }
                battleshipLives--;

                if (battleshipLives == 0)
                {
                    direction = 'X';
                }
              }

              else if (report == 'S')
              {
                if (submarineLives == 3)
                {
                    firstHitGrid.x = attackGrid.x;
                    firstHitGrid.y = attackGrid.y;
                    direction = 'N';
                }
                submarineLives--;
                                    
                if (submarineLives == 0)
                {
                    direction = 'X';
                }
              }
            
              else if (report == 'C')
              {
                if (carrierLives == 5)
                {
                    firstHitGrid.x = attackGrid.x;
                    firstHitGrid.y = attackGrid.y;
                    direction = 'N';
                }
                carrierLives--;

                if (carrierLives == 0)
                {
                    //return; 
                    direction = 'X';
                }
              }

              else if (report == 'D')
              {
                if (destroyerLives == 3)
                {
                    firstHitGrid.x = attackGrid.x;
                    firstHitGrid.y = attackGrid.y;
                    direction = 'N';
                }
                destroyerLives--;
                                    
                if (destroyerLives == 0)
                {
                    direction = 'X';
                }
              }

              else if (report == 'P')
              {
                if (patrolLives == 2)
                {
                    firstHitGrid.x = attackGrid.x;
                    firstHitGrid.y = attackGrid.y;
                    direction = 'N';
                }
                patrolLives--;

                if (patrolLives == 0)
                { 
                    direction = 'X';
                }
              }

              if (direction == 'N' && attackGrid.y - 1 == 0 && attackHistory[attackGrid.x, attackGrid.y - 1] == ShipType.None)
              {
                  direction = 'N'; // are these four chunks of code correct with the x - 1 vs. y - 1?
              }

              else if (direction == 'S' && attackGrid.y + 1 == 0 && attackHistory[attackGrid.x, attackGrid.y + 1] == ShipType.None)
              {
                  direction = 'S';
              }

              else if (direction == 'E' && attackGrid.x + 1 == 0 && attackHistory[attackGrid.x + 1, attackGrid.y] == ShipType.None)
              {
                  direction = 'E';
              }

              else if (direction == 'W' && attackGrid.x - 1 == 0 && attackHistory[attackGrid.x - 1, attackGrid.y] == ShipType.None)
              {
                  direction = 'W'; //
              }
            }
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            Random rng = new Random();
            myFleet.Carrier    = new ShipPosition(4, rng.Next(2, 5), ShipRotation.Horizontal); // 5 long
            myFleet.Battleship = new ShipPosition(0, rng.Next(6, 9), ShipRotation.Horizontal); // 4 long
            myFleet.Destroyer  = new ShipPosition(0, rng.Next(2, 5), ShipRotation.Horizontal); // 3 long
            myFleet.Submarine  = new ShipPosition(5, rng.Next(6, 9), ShipRotation.Horizontal); //3 long
            myFleet.PatrolBoat = new ShipPosition(rng.Next(5, 9), 0, ShipRotation.Vertical); // 2 long

            return myFleet;
        }
    }
}
