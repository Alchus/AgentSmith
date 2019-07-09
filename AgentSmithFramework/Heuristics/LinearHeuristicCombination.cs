using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgentSmith.Framework;

namespace AgentSmithFramework.Heuristics
{
    public class LinearHeuristicCombination : Heuristic
    {
        private List<Tuple<Heuristic, double>> Components = new List<Tuple<Heuristic, double>>();

        public LinearHeuristicCombination(params Tuple<Heuristic, double>[] heurs)
        {
            Add(heurs);
        }

        public void Clear() => Components.Clear();

        public void Add(params Tuple<Heuristic, double>[] heurs)
        {
            Components.AddRange(heurs);
            var sum = Components.Select(x => x.Item2).Sum();

            Components = Components.Select(tup => new Tuple<Heuristic, double>(tup.Item1, tup.Item2 / sum)).ToList();
        }

        public override double Value(Game game, Agent player)
        {
            return game.GetScore(player);
        }
    }
}
