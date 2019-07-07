using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentSmith.Framework;
using MoreLinq;

namespace AgentSmith.Agents
{
    public class RandomPlayAgent : Agent
    {

        public RandomPlayAgent(string name, int seed = 0) : base(name)
        {
            Rand = seed != 0 ? new Random(seed) : new Random();
        }

        private Random Rand { get; set; }
        public override Move GetMove(Game game)
        {
            return game.GetMoves().Shuffle(Rand).First();
        }
    }
}
