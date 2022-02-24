using System;

namespace Battleship
{
    public class SuperCoolAgent : BattleshipAgent
    {
        char[,] attackHistory;
        GridSquare attackSq;

        char lastHit;
        const char untried = '_';
        
        int jumpSize;
        bool searchByRow;



        public SuperCoolAgent()
        {
            attackHistory = new char[10, 10];
            attackSq = new GridSquare();

            jumpSize = 3;
            searchByRow = true;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    attackHistory[i, j] = untried; // finding untried square
                }
            }

        }

        public override string ToString()
        {
            return $"Battleship Agent '{GetNickname()}'";
        }

        public override string GetNickname()
        {
            return "PrisonMike";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }
        
        public override GridSquare LaunchAttack()
        {
            
            if (lastHit != '\0')
            {
               if (attackSq.x > 0) 
                {
                    attackSq.x -= 1;
                    return attackSq;
                }
                if (attackSq.x == 0)
                {
                    attackSq.y -= 1;
                    return attackSq;
                } 
                
            }
            
            if (lastHit == '\0')
            {
               
                Random rnd = new Random();
                attackSq.x = rnd.Next(0, 10);
                attackSq.y = rnd.Next(0, 10);
                

                while (attackHistory[attackSq.x, attackSq.y] != untried)
                {
                    attackSq.x = rnd.Next(0, 10);
                    attackSq.y = rnd.Next(0, 10);
                }
            }
            else
            {
                FindFirstFreeSq();
            }

            return attackSq;
        }
        
        private void FindFirstFreeSq()
        {
            
            if (searchByRow)
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j += jumpSize)
                    {

                        if (attackHistory[i, j] != untried)
                        {
                            attackSq.x = i; 
                            attackSq.y = j;
                            return;
                        }
                    }
                }
            }
  
            else
            {
                for (int a = 0; a < 10; a += jumpSize)
                {
                    for (int b = 0; b < 10; b += 1)
                    {
                        if (attackHistory[a, b] != untried)
                        {
                            attackSq.x = a;
                            attackSq.y = b;
                            return;
                        }
                    }
                }
            }

            if (searchByRow)
            {
                searchByRow = false;
            }

            else
            {
                jumpSize = 1;
                FindFirstFreeSq();
            }
            
        }
    
        public override void DamageReport(char report)
        {
            attackHistory[attackSq.x, attackSq.y] = report;
            lastHit = report;
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();
            Random rnd = new Random();

            myFleet.Carrier = new ShipPosition(rnd.Next(4, 5), rnd.Next(6, 9), ShipRotation.Horizontal);
            myFleet.Battleship = new ShipPosition(rnd.Next(0, 3), rnd.Next(0, 1), ShipRotation.Vertical);
            myFleet.Destroyer = new ShipPosition(rnd.Next(4, 9), 0, ShipRotation.Vertical);
            myFleet.Submarine = new ShipPosition(rnd.Next(0, 1), rnd.Next(6, 9), ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(rnd.Next(5, 8), rnd.Next(3, 5), ShipRotation.Horizontal);

            return myFleet;
        }
    }
}
