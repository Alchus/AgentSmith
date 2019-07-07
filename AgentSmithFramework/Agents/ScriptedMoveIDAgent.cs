using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using AgentSmith.Framework;
using MoreLinq;

namespace AgentSmith.Agents
{
    public class ScriptedMoveIDAgent : Agent
    {
        private List<int> Script;
        public ScriptedMoveIDAgent(string name, IEnumerable<int> script) : base(name)
        {
            Script = script.ToList();
        }

        public override Move GetMove(Game game)
        {
            if (Script.Count == 0) throw new ApplicationException("Script agent is out of moves");
            var choice = Script[0];
            Script.RemoveAt(0);
            return game.GetMoves().First(move => move.ID == choice);
        }
    }
}
