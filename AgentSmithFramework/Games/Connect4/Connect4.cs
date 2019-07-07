using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AgentSmith.Framework;
using AgentSmithFramework.Framework;

namespace AgentSmith.Games.Connect4
{
    public class Connect4 : Game
    {
        public static readonly int BoardHeight = 6;
        public static readonly int BoardWidth = 7;
        public Connect4(Agent[] players) : base (players)
        {

        }
        private Connect4(Agent[] players, State state) : base(players, state)
        {

        }

        private char GetChar(Agent player)
        {
            if (player == Players[0]) return 'X';
            if (player == Players[1]) return 'O';
            throw new ArgumentException();
        }

        
        private char[,] board => ((Connect4State) State).board;
        
        public override double GetScore(Agent player)
        {

            if (GetChar(player) == 'X')
            {

                if (AreFourConnected('X'))
                    return 1.0;
                if (AreFourConnected('O'))
                    return 0.0;

            }

            if (GetChar(player) == 'O')
            {

                if (AreFourConnected('O'))
                    return 1.0;
                if (AreFourConnected('X'))
                    return 0.0;

            }
            return 0.5;
        }

        //"Borrowed" from https://stackoverflow.com/a/38211417
        private bool AreFourConnected(char player)
        {

            // horizontalCheck 
            for (int x = 0; x < BoardWidth - 4; x++)
            {
                for (int y = 0; y < BoardHeight -1; y++)
                {
                    if (board[x, y] == player && board[x + 1, y] == player && board[x + 2, y] == player && board[x + 3, y] == player)
                    {
                        return true;
                    }
                }
            }
            // verticalCheck
            for (int y = 0; y < BoardHeight - 4; y++)
            {
                for (int x = 0; x < BoardWidth -1; x++)
                {
                    if (board[x, y] == player && board[x, y + 1] == player && board[x, y + 2] == player && board[x, y + 3] == player)
                    {
                        return true;
                    }
                }
            }
            // ascendingDiagonalCheck 
            for (int y = 3; y < BoardHeight -1; y++)
            {
                for (int x = 0; x < BoardWidth - 4; x++)
                {
                    if (board[x,y] == player && board[x + 1,y - 1] == player && board[x + 2,y - 2] == player && board[x + 3,y - 3] == player)
                        return true;
                }
            }
            // descendingDiagonalCheck
            for (int y = 3; y < BoardHeight - 1; y++)
            {
                for (int x = 3; x < BoardWidth - 1; x++)
                {
                    if (board[x,y] == player && board[x - 1,y - 1] == player && board[x - 2,y - 2] == player && board[x - 3,y - 3] == player)
                        return true;
                }
            }
            return false;
        }

        public override bool IsFinished()
        {
            return (GetScore(Players[0]) != 0.5) ||
                   Enumerable.Range(0, BoardWidth).All(x => board[x, BoardHeight - 1] != '-');
        }

        public override String ToString()
        {
            return
                "1|2|3|4|5|6|7\n"+
                $"{board[0, 5]},{board[1, 5]},{board[2, 5]},{board[3, 5]},{board[4, 5]},{board[5, 5]},{board[6, 5]},\n" +
                $"{board[0, 4]},{board[1, 4]},{board[2, 4]},{board[3, 4]},{board[4, 4]},{board[5, 4]},{board[6, 4]},\n" +
                $"{board[0, 3]},{board[1, 3]},{board[2, 3]},{board[3, 3]},{board[4, 3]},{board[5, 3]},{board[6, 3]},\n" +
                $"{board[0, 2]},{board[1, 2]},{board[2, 2]},{board[3, 2]},{board[4, 2]},{board[5, 2]},{board[6, 2]},\n" +
                $"{board[0, 1]},{board[1, 1]},{board[2, 1]},{board[3, 1]},{board[4, 1]},{board[5, 1]},{board[6, 1]},\n" +
                $"{board[0, 0]},{board[1, 0]},{board[2, 0]},{board[3, 0]},{board[4, 0]},{board[5, 0]},{board[6, 0]},\n" +
                (IsFinished()
                    ? $"Game complete, Score: {GetScore(Players[0])}"
                    : "Game in progress" + "\n" +
                      $"> {CurrentPlayer} to play\n");
        }

        public override IEnumerable<Move> GetMoves()
        {
            return Enumerable.Range(1, BoardWidth).Where(x => board[x-1,BoardHeight-1].Equals('-')).Select(i => new Move(i));
        }


        public override void Setup()
        {
            var state = new Connect4State
            {
                board = new[,]
                {
                    {'-', '-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-', '-'},
                    {'-', '-', '-', '-', '-', '-'}
                },
                CurrentPlayer = 0
            };

            State = state;
        }
    
        public override bool ApplyMove(Move move)
        {
            var x = move.ID - 1;
            var y = Enumerable.Range(0, 6).First(y_ => board[x, y_] == '-');
            board[x,y] = GetChar(CurrentPlayer);

            State.CurrentPlayer = (State.CurrentPlayer + 1) % 2;
            return true;
        }
    }

    public class Connect4State : State
    {

        public char[,] board { get; set; }
        
        public override object Clone()
        {
            var o = (Connect4State)MemberwiseClone();
            o.board = (char[,]) board.Clone();
            return o;
        }
    }
    
}
