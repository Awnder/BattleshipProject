using System;

namespace Battleship
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfRounds = 15;
            int roundDelay = 800; // milliseconds
            int shotDelay = 50; // milliseconds

            //BattleshipAgent myBattleshipAgent = new OriginalAgent();
            //BattleshipAgent myBattleshipAgent = new RandomSpacedAgent();
            BattleshipAgent myBattleshipAgent = new SquareSpiralAgent();
            //BattleshipAgent myBattleshipAgent = new VerticalAgent();
            //BattleshipAgent myTestingOpponent = new HumanPlayer();
            BattleshipAgent myTestingOpponent = new BozoTheClown();
            //BattleshipAgent myTestingOpponent = new LarryTheLine();

            BattleshipEngine gameEngine = new BattleshipEngine(myBattleshipAgent, myTestingOpponent);
            for (int i = 0; i < numberOfRounds; i++)
            {
                BattleshipAgent winner = gameEngine.PlaySingleGame(true, shotDelay);
                System.Threading.Thread.Sleep(roundDelay);
            }
        }
    }
}
