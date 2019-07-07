using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentSmith.Framework;

namespace AgentSmithFramework.Agents
{
    public class MiniMaxAgent : Agent
    {
        public MiniMaxAgent(string name) : base(name)
        {
        }

        public override Move GetMove(Game game)
        {
            GetMax(game, out var move);
            return move;
        }

        public double GetMax(Game game, out Move bestMove)
        {
            bestMove = null;
            var bestSoFar = double.MinValue;
            foreach (var move in game.GetMoves())
            {
                var future = game.GetSuccessor(move);
                var value = (future.IsFinished()) 
                    ? future.GetScore(this) 
                    : GetMin(future, out var worst);
                if (value > bestSoFar)
                {
                    bestSoFar = value;
                    bestMove = move;
                }
            }

            return bestSoFar;
        }

        private double GetMin(Game game, out Move bestMove)
        {
            bestMove = null;
            var bestSoFar = double.MaxValue;
            foreach (var move in game.GetMoves())
            {
                var future = game.GetSuccessor(move);
                var value = (future.IsFinished())
                    ? future.GetScore(this)
                    : GetMax(future, out var worst);
                if (value < bestSoFar)
                {
                    bestSoFar = value;
                    bestMove = move;
                }
            }

            return bestSoFar;
        }
    }
}
