using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;
using AgentSmith.Framework;
using AgentSmithFramework.Framework;

namespace AgentSmith.Games.TicTacToe
{
    public class TicTacToe : Game
    {
        public TicTacToe(Agent[] players) : base (players)
        {

        }
        private TicTacToe(Agent[] players, State state) : base(players, state)
        {

        }

        private char GetChar(Agent player)
        {
            if (player == Players[0]) return 'X';
            if (player == Players[1]) return 'O';
            throw new ArgumentException();
        }

        static readonly int[][] lines = new int[][]
        {
            new[]{0, 1, 2},
            new[]{3, 4, 5},
            new[]{6, 7, 8},
            new[]{0, 3, 6},
            new[]{1, 4, 7},
            new[]{2, 5, 8},
            new[]{0, 4, 8},
            new[]{2, 4, 6}
        };

        private char[] board => ((TicTacToeState) State).board;
        
        public override double GetScore(Agent player)
        {
            
            if (player.Equals(Players[0]))
                foreach (var line in lines)
                {
                    if (line.All(index => board[index] == 'X'))
                        return 1.0;
                    if (line.All(index => board[index] == 'O'))
                        return 0.0;
                }
            if (player.Equals(Players[1]))
                foreach (var line in lines)
                {
                    if (line.All(index => board[index] == 'X'))
                        return 0.0;
                    if (line.All(index => board[index] == 'O'))
                        return 1.0;
                }

            return 0.5;
        }

        public override bool IsFinished()
        {
            return (GetScore(Players[0]) != 0.5) || board.All(entry => entry != '-');
        }

        public override String ToString()
        {
            return
                $"{board[6]},{board[7]},{board[8]}\n" +
                $"{board[3]},{board[4]},{board[5]}\n" +
                $"{board[0]},{board[1]},{board[2]}\n" +
                (IsFinished()
                    ? $"Game complete, Score: {GetScore(Players[0])}"
                    : "Game in progress" + "\n" +
                      $"> {CurrentPlayer} to play\n");
        }

        
        public override IEnumerable<Move> GetMoves()
        {
            return Enumerable.Range(1, 9).Where(x => board[x-1].Equals('-')).Select(i => new Move(i));
        }


        public override void Setup()
        {
            var state = new TicTacToeState
            {
                board = new[] {'-', '-', '-', '-', '-', '-', '-', '-', '-'},
                CurrentPlayer = 0
            };

            State = state;
        }
    
        public override bool ApplyMove(Move move)
        {
            board[move.ID - 1] = GetChar(CurrentPlayer);

            State.CurrentPlayer = (State.CurrentPlayer + 1) % 2;
            return true;
        }

    }

    public class TicTacToeState : State
    {
        

        public char[] board { get; set; }
        
        public override object Clone()
        {
            var o = (TicTacToeState)MemberwiseClone();
            o.board = (char[]) board.Clone();
            return o;
        }
    }
    
}
