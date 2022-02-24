using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfRounds = 1;
            int roundDelay = 1; // milliseconds
            int shotDelay = 1; // milliseconds

            //BattleshipAgent myBattleshipAgent = new OriginalAgent();
            //BattleshipAgent myBattleshipAgent = new RandomSpacedAgent();
            //BattleshipAgent myBattleshipAgent = new SquareSpiralAgent();
            //BattleshipAgent myBattleshipAgent = new VerticalAgent();
            BattleshipAgent myBattleshipAgent = new HeatAgent();

            //BattleshipAgent myTestingOpponent = new HumanPlayer();
            //BattleshipAgent myTestingOpponent = new BozoTheClown();
            //BattleshipAgent myTestingOpponent = new LarryTheLine();

            //BattleshipAgent myTestingOpponent = new BotEdgePlacement();
            BattleshipAgent myTestingOpponent = new BotCenterPlacement();
            //BattleshipAgent myTestingOpponent = new BotRandomPlacement();
            //BattleshipAgent myTestingOpponent = new SuperCoolAgent();


            BattleshipEngine gameEngine = new BattleshipEngine(myBattleshipAgent, myTestingOpponent);
            for (int i = 0; i < numberOfRounds; i++)
            {
                string winner = gameEngine.PlaySingleGame(true, shotDelay);
                System.Threading.Thread.Sleep(roundDelay);
            }
        }
    }
}
