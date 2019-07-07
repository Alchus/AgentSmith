using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgentSmithFramework.Framework
{
    public abstract class State : ICloneable
    {
        public int CurrentPlayer;
        public abstract object Clone();
    }
}
