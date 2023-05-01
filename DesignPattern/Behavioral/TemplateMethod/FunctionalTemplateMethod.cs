using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;

namespace DesignPattern.Behavioral.TemplateMethod
{
    [TestClass]
    public class FunctionalTemplateMethod : IExecute
    {
        [TestMethod]
        public void Execute()
        {
            var numberOfPlayers = 2;
            int currentPlayer = 0;
            int turn = 1, maxTurns = 10;

            void Start()
            {
                WriteLine($"Starting a game of chess with {numberOfPlayers} players.");
            }

            bool HaveWinner()
            {
                return turn == maxTurns;
            }

            void TakeTurn()
            {
                WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
                currentPlayer = (currentPlayer + 1) % numberOfPlayers;
            }

            int WinningPlayer()
            {
                return currentPlayer;
            }

            GameTemplate.Run(Start, TakeTurn, HaveWinner, WinningPlayer);
        }

        public static class GameTemplate
        {
            public static void Run(
                Action start,
                Action takeTurn,
                Func<bool> haveWinner,
                Func<int> winningPlayer)
            {
                start();
                while (!haveWinner())
                    takeTurn();
                WriteLine($"Player {winningPlayer()} wins.");
            }
        }
    }
}
