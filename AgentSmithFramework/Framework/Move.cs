using System;
using System.Collections.Generic;
using System.Text;

namespace AgentSmith.Framework
{
    public class Move
    {
        public Move(int id)
        {
            ID = id;
        }
        public int ID { get; set; }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
