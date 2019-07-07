//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using AgentSmith.Framework;
//using System.ComponentModel;
//using System.Drawing;
//using SmithUI;

//namespace AgentSmith.Agents
//{
    
//    public class HumanAgent : Agent
//    {
//        private HumanPlayer form;
        
//        public HumanAgent(string name, bool useNewConsole = false) : base(name)
//        {
//            form = new HumanPlayer();
//        }

//        public override Move GetMove(Game game)
//        {
//            form.ClearText();
//            form.WriteLine(game);
//            form.WriteLine($">>> Select move for human player {this}");
//            form.ClearOptions();
//            var moves = game.GetMoves().ToArray();

//            foreach (var move in moves)
//            {
//                form.AddOption(move.ID, move.ToString());
//            }

       
//            form.Write(">");
//            var input = form.GetOption();
//            var selection = moves.FirstOrDefault(move => move.ID == input);
//            if (selection != null)
//            {
//                return selection;
//            }
//            throw new ApplicationException("Invalid Input");
//        }
//    }
//}
