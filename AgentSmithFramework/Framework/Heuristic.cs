using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentSmith.Framework;

namespace AgentSmithFramework.Heuristics
{
    public abstract class Heuristic
    {
        public double Value(Game game, int playerNo) => Value(game, game.Players[playerNo]);
        public abstract double Value(Game game, Agent player);
    }
}
