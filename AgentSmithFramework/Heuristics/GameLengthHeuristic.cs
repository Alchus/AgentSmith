using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentSmith.Framework;

namespace AgentSmithFramework.Heuristics
{
    class CurrentScoreHeuristic : Heuristic
    {
        public override double Value(Game game, Agent player)
        {
            return game.GetScore(player);
        }
    }
}
