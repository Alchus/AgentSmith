using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentSmith.Framework;
using AgentSmithFramework.Heuristics;
using MoreLinq;
using MoreLinq.Extensions;

namespace AgentSmithFramework.Agents
{
    public class MiniMaxAgent : Agent
    {
        public Heuristic heuristic = new CurrentScoreHeuristic();
        private int DepthLimit;
        public MiniMaxAgent(string name, int depthLimit = 9) : base(name)
        {
            DepthLimit = depthLimit;
        }

        public override Move GetMove(Game game)
        {
            NegaMax(game, 0, out var move, double.NegativeInfinity, double.PositiveInfinity, 1.0);
            //GetMax(game, 0, out var move, double.NegativeInfinity, double.PositiveInfinity);
            return move;
        }

        private double NegaMax_MT(Game game, int depth, out Move bestMove, double multiplier = 1.0)
        {
            bestMove = null;
            Move _bestMove = null;
            if (depth == DepthLimit) return multiplier * heuristic.Value(game, this);

            var bestSoFar =  double.NegativeInfinity;

            var tuples = game.GetMoves()
                .AsParallel()
                .Select(move =>
                {
                    var future = game.GetSuccessor(move);
                    var value = (future.IsFinished())
                        ? future.GetScore(this)
                        : NegaMax_MT(future, depth + 1, out var worst, -1.0 * multiplier);
                    return new Tuple<double, Move>(value, move);
                }).AsEnumerable();

            foreach (var t in tuples)
            {
                if (t.Item1 > bestSoFar)
                {
                    bestSoFar = t.Item1;
                    _bestMove = t.Item2;
                }
            }
                
            bestMove = _bestMove;
            return bestSoFar;
        }

        private double NegaMax(Game game, int depth, out Move bestMove, double alpha, double beta, double multiplier = 1.0)
        {
            bestMove = null;

            if (game.IsFinished()) return multiplier * game.GetScore(this);

            if (depth == DepthLimit) return multiplier * heuristic.Value(game, this);

            var bestSoFar = double.NegativeInfinity;

            foreach (var move in game.GetMoves())
            {
                var future = game.GetSuccessor(move);
                var value = -1.0 * NegaMax(future, depth + 1, out var worst, -1.0 * beta, -1.0 * alpha, -1.0 * multiplier);
                if (value >= bestSoFar)
                {
                    bestSoFar = value;
                    bestMove = move;
                    //alpha = Math.Max(alpha, value);
                }

                //if (alpha >= beta)
                //{
                //    //break;
                //}
            }

            return bestSoFar;
        }

        //Synchronous with pruning
        #region Synchronous

        public double GetMax(Game game, int depth, out Move bestMove, double alpha, double beta)
        {
            bestMove = null;
            if (game.IsFinished()) return game.GetScore(this);

            if (depth == DepthLimit) return heuristic.Value(game, this);
            var bestSoFar = double.NegativeInfinity;
            foreach (var move in game.GetMoves())
            {
                if (bestSoFar > beta) return bestSoFar;
                var future = game.GetSuccessor(move);
                var value = GetMin(future, depth + 1, out var worst, alpha, beta);
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
            if (game.IsFinished()) return game.GetScore(this);

            if (depth == DepthLimit) return heuristic.Value(game, this);
            var bestSoFar = double.PositiveInfinity;
            foreach (var move in game.GetMoves())
            {
                if (bestSoFar < alpha)
                    return bestSoFar;
                var future = game.GetSuccessor(move);
                var value = GetMax(future, depth + 1, out var best, alpha, beta);
                if (value < bestSoFar)
                {
                    bestSoFar = value;
                    beta = bestSoFar;
                    bestMove = move;
                }
            }

            return bestSoFar;
        }

        #endregion

    }
}
