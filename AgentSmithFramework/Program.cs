using System;
using AgentSmith.Agents;
using AgentSmith.Framework;
using AgentSmith.Games.TicTacToe;
using AgentSmith.Games.Connect4;
using AgentSmithFramework.Agents;

namespace AgentSmith
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayConnect4Interactive();
        }

        public static void PlayTicTacToeInteractive()
        {
            Agent X = new HumanAgent("Thomas");
            Agent O = new MiniMaxAgent("Brutus");

            var game = new TicTacToe(new[] { X, O });
            game.Run();

             Console.WriteLine(game);
             Console.WriteLine("Done!");
        }

        public static void PlayConnect4Interactive()
        {
            Agent X = new HumanAgent("Thomas");
            Agent O = new MiniMaxAgent("Brutus");

            var game = new Connect4(new[] { X, O });
            game.Run();

            Console.WriteLine(game);
            Console.WriteLine("Done!");
        }
    }
}
