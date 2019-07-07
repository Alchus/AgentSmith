using System;
using System.Collections.Generic;
using System.Text;
using AgentSmithFramework.Framework;

namespace AgentSmith.Framework
{


    public abstract class Game : ICloneable
    {
        public Agent CurrentPlayer => Players[State.CurrentPlayer];
        public Agent[] Players;

        public Game GetSuccessor(Move move)
        {
            Game g = (Game)this.Clone();
            g.ApplyMove(move);
            return g;
        }

        public object Clone()
        {
            var g = (Game) this.MemberwiseClone();
            g.State = (State) g.State.Clone();
            return g;
        }

        public abstract void Setup();
        public abstract bool ApplyMove(Move move);
        public abstract bool IsFinished();
        public abstract double GetScore(Agent player);
        public abstract IEnumerable<Move> GetMoves();

        public State State;

        public object HiddenState;

        protected Game(Agent[] players)
        {
            Players = players;
            Setup();
        }
        protected Game(Agent[] players, State state = null)
        {
            Players = players;
            if (state != null) State = (State)state.Clone();
        }

        public void Run()
        {
            while (!IsFinished())
            {
                ApplyMove(CurrentPlayer.GetMove(this));

            }
        }

        
    }
}

