using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentSmith.Framework;
using AgentSmithFramework.Heuristics;
using MoreLinq;

namespace AgentSmithFramework.Agents
{
    public class MiniMaxAgent : Agent
    {
        public Heuristic heuristic = new CurrentScoreHeuristic();
        private int DepthLimit;
        public MiniMaxAgent(string name, int depthLimit = 7) : base(name)
        {
            DepthLimit = depthLimit;
        }

        public override Move GetMove(Game game)
        {
            GetMax(game,0, out var move, double.MinValue, double.MaxValue);
            return move;
        }

        public double GetMax(Game game, int depth, out Move bestMove, double alpha, double beta)
        {
            bestMove = null;
            if (depth == DepthLimit) return heuristic.Value(game, this);
            var bestSoFar = double.NegativeInfinity;
            foreach (var move in game.GetMoves())
            {
                if (bestSoFar > beta) return bestSoFar;
                var future = game.GetSuccessor(move);
                var value = (future.IsFinished()) 
                    ? future.GetScore(this) 
                    : GetMin(future, depth + 1, out var worst, alpha , beta);
                if (value > bestSoFar)
                {
                    bestSoFar = value;
                    alpha = value;
                    bestMove = move;
                }
            }

            return bestSoFar;
        }

        private double GetMin(Game game, int depth, out Move bestMove, double alpha, double beta)
        {
            bestMove = null;
            if (depth == DepthLimit) return heuristic.Value(game, this);
            var bestSoFar = double.PositiveInfinity;
            foreach (var move in game.GetMoves())
            {
                if (bestSoFar < alpha)
                    return bestSoFar;
                var future = game.GetSuccessor(move);
                var value = (future.IsFinished())
                    ? future.GetScore(this)
                    : GetMax(future,depth + 1, out var best, alpha, beta);
                if (value < bestSoFar)
                {
                    bestSoFar = value;
                    beta = bestSoFar;
                    bestMove = move;
                }
            }

            return bestSoFar;
        }
    }
}
