using System;

namespace Battleship
{
    public class SuperCoolAgent : 2019_Fall_ErinThompson_Battleship
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
            return "Explorer";
        }

        public override void SetOpponent(string opponent)
        {
            return;
        }

        public override GridSquare LaunchAttack()
        {
            while (attackHistory[attackGrid.x, attackGrid.y] != ShipType.None)
            {
                Random a = new Random();
                attackGrid.x = a.Next(0,10);
                attackGrid.y = a.Next(0,10);

                while (attackHistory[attackGrid.x, attackGrid.y] == 'C')
                {
                    attackGrid.x = attackGrid.x + 1;
                    if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                    {
                        attackGrid.x = attackGrid.x - 2;
                        if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                        {
                            attackGrid.x = attackGrid.x + 1;
                            attackGrid.y = attackGrid.y + 1;
                            if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                            {
                                attackGrid.y = attackGrid.y - 2;
                            }
                        }
                        return attackGrid;
                    }

                }
                while (attackHistory[attackGrid.x, attackGrid.y] == 'B')
                {
                    attackGrid.x = attackGrid.x + 1;
                    if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                    {
                        attackGrid.x = attackGrid.x - 2;
                        if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                        {
                            attackGrid.x = attackGrid.x + 1;
                            attackGrid.y = attackGrid.y + 1;
                            if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                            {
                                attackGrid.y = attackGrid.y - 2;
                            }
                        }
                        return attackGrid;
                    }
                    
                }
                while (attackHistory[attackGrid.x, attackGrid.y] == 'D')
                {
                    attackGrid.x = attackGrid.x + 1;
                    if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                    {
                        attackGrid.x = attackGrid.x - 2;
                        if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                        {
                            attackGrid.x = attackGrid.x + 1;
                            attackGrid.y = attackGrid.y + 1;
                            if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                            {
                                attackGrid.y = attackGrid.y - 2;
                            }
                        }
                        return attackGrid;
                    }
                    
                }
                while (attackHistory[attackGrid.x, attackGrid.y] == 'S')
                {
                    attackGrid.x = attackGrid.x + 1;
                    if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                    {
                        attackGrid.x = attackGrid.x - 2;
                        if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                        {
                            attackGrid.x = attackGrid.x + 1;
                            attackGrid.y = attackGrid.y + 1;
                            if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                            {
                                attackGrid.y = attackGrid.y - 2;
                            }
                        }
                        return attackGrid;
                    }
                    
                }
                while (attackHistory[attackGrid.x, attackGrid.y] == 'P')
                {
                    attackGrid.x = attackGrid.x + 1;
                    if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                    {
                        attackGrid.x = attackGrid.x - 2;
                        if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                        {
                            attackGrid.x = attackGrid.x + 1;
                            attackGrid.y = attackGrid.y + 1;
                            if (attackHistory[attackGrid.x, attackGrid.y] == ShipType.None)
                            {
                                attackGrid.y = attackGrid.y - 2;
                            }
                        }
                    }
                }
                return attackGrid;
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
                attackHistory[attackGrid.x, attackGrid.y] = report;
            }
            
        }

        public override BattleshipFleet PositionFleet()
        {
            BattleshipFleet myFleet = new BattleshipFleet();

            Random r = new Random();

            myFleet.Carrier    = new ShipPosition(r.Next(0,3), r.Next(3,5), ShipRotation.Vertical);
            myFleet.Battleship = new ShipPosition(r.Next(2,5), r.Next(0,3), ShipRotation.Horizontal);
            myFleet.Destroyer  = new ShipPosition(r.Next(4,7), r.Next(6,7), ShipRotation.Vertical);
            myFleet.Submarine  = new ShipPosition(r.Next(7,8), r.Next(6,9), ShipRotation.Horizontal);
            myFleet.PatrolBoat = new ShipPosition(r.Next(4,9), r.Next(3,5), ShipRotation.Vertical);

            return myFleet;
        }
    }
}
