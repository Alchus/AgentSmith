using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using AgentSmith.Framework;
using AgentSmithFramework.Framework;

namespace AgentSmith.Games.TreeTraversal
{
    public class TreeTraversal : Game
    {
        public TreeTraversal(Agent[] players) : base (players)
        {

        }
        private TreeTraversal(Agent[] players, State state) : base(players, state)
        {

        }


        
        private double[] options
        {
            get => ((TreeTraversalState)State).options;
            set => ((TreeTraversalState)State).options = value;
        }

        public override double GetScore(Agent player)
        {

            if (IsFinished()) return options[0];
            return 0;
        }

        public override bool IsFinished()
        {
            return (options.Length == 1);
        }

        public override string ToString()
        {
            return
                options.Select(x => x.ToString()).Aggregate((a,  s) => a + s ) + "\n" + 
                (IsFinished()
                    ? $"Game complete, Score: {GetScore(Players[0])}"
                    : "Game in progress" + "\n" +
                      $"> {CurrentPlayer} to play\n");
        }

        
        public override IEnumerable<Move> GetMoves()
        {
            return Enumerable.Range(0, 2).Select(x => new Move(x));
        }


        public override void Setup()
        {
            var state = new TreeTraversalState
            {
                options = new double[] {4,6,7,9,1,2,0,1},
                CurrentPlayer = 0
            };

            State = state;
        }
    
        public override bool ApplyMove(Move move)
        {
            if (move.ID == 0)
            {
                options = options.Take(options.Length / 2).ToArray();
            }
            else
            {
                options = options.Skip(options.Length / 2).ToArray();
            }
            
            State.CurrentPlayer = (State.CurrentPlayer + 1) % 2;
            return true;
        }

    }

    public class TreeTraversalState : State
    {
        public double[] options;
        
        public override object Clone()
        {
            var o = (TreeTraversalState)MemberwiseClone();
            o.options = (double[]) options.Clone();
            return o;
        }
    }
    
}
