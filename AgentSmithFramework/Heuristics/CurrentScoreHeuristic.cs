using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentSmith.Framework;
using AgentSmithFramework.Framework;

namespace AgentSmithFramework.Heuristics
{
    class GameLengthHeuristic : Heuristic
    {
        public double value = 1.0;
        public override double Value(Game game, Agent player)
        {
            return ((MovesCount)game.State).MoveCount() * value;
        }
    }
}
