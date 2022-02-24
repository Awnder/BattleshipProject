using System;
using System.Collections.Generic;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfRounds = 10;
            int roundDelay = 800; // milliseconds
            int shotDelay = 10; // milliseconds

            //BattleshipAgent myBattleshipAgent = new OriginalAgent();
            //BattleshipAgent myBattleshipAgent = new RandomSpacedAgent();
            //BattleshipAgent myBattleshipAgent = new SquareSpiralAgent();
            //BattleshipAgent myBattleshipAgent = new VerticalAgent();
            BattleshipAgent myBattleshipAgent = new HeatAgent();

            //BattleshipAgent myTestingOpponent = new HumanPlayer();
            //BattleshipAgent myTestingOpponent = new BozoTheClown();
            //BattleshipAgent myTestingOpponent = new LarryTheLine();

            //BattleshipAgent myTestingOpponent = new BotEdgePlacement();
            //BattleshipAgent myTestingOpponent = new BotCenterPlacement();
            BattleshipAgent myTestingOpponent = new BotRandomPlacement();


            BattleshipEngine gameEngine = new BattleshipEngine(myBattleshipAgent, myTestingOpponent);
            for (int i = 0; i < numberOfRounds; i++)
            {
                string winner = gameEngine.PlaySingleGame(true, shotDelay);
                System.Threading.Thread.Sleep(roundDelay);
            }

            //testing HeatAgent
            List<BattleshipAgent> agents = new List<BattleshipAgent>();


            //for (int i = 0; i < )
            //BattleshipEngine gameEngine = new BattleshipEngine();
        }
    }
}
