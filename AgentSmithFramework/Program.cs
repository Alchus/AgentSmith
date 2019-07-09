using System;
using AgentSmith.Agents;
using AgentSmith.Framework;
using AgentSmith.Games.TicTacToe;
using AgentSmith.Games.Connect4;
using AgentSmithFramework.Agents;
using AgentSmithFramework.Heuristics;

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
            var X = new HumanAgent("Thomas");
            var O = new MiniMaxAgent("Brutus");

            O.heuristic = new LinearHeuristicCombination(
                new Tuple<Heuristic, double>(new CurrentScoreHeuristic(), 1.0),
                new Tuple<Heuristic, double>(new GameLengthHeuristic(), 0.01));

            var game = new TicTacToe(new Agent[] { X, O });
            game.Run();

             Console.WriteLine(game);
             Console.WriteLine("Done!");
        }

        public static void PlayConnect4Interactive()
        {
            Agent O = new HumanAgent("Thomas");
            Agent X = new MiniMaxAgent("Brutus");

            var game = new Connect4(new[] { X, O });
            game.Run();

            Console.WriteLine(game);
            Console.WriteLine("Done!");
        }
    }
}
