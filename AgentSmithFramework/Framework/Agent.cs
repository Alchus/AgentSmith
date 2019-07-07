using System;
using System.Collections.Generic;
using System.Text;

namespace AgentSmith.Framework
{
    public abstract class Agent
    {

        public string Name { get; set; }

        public abstract Move GetMove(Game game);


        public Agent(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
