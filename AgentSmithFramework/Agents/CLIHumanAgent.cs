using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using AgentSmith.Framework;
using System.ComponentModel;
using System.Drawing;

namespace AgentSmith.Agents
{

    public class HumanAgent : Agent
    {

        public HumanAgent(string name, bool useNewConsole = false) : base(name)
        {
        }

        public override Move GetMove(Game game)
        {
            Console.WriteLine(game);
            Console.WriteLine($">>> Select move for human player {this}");
            var moves = game.GetMoves().ToArray();

            foreach (var move in moves)
            {
                Console.WriteLine($"{move.ID}) {move}");
            }

            


            while (true)
            {
                Console.Write(">");
                var input = Console.ReadLine();

                if (input == "") return moves.First();

                if (int.TryParse(input, out var value))
                {
                    var selection = moves.FirstOrDefault(move => move.ID == value);
                    if (selection != null)
                    {
                        return selection;
                    }
                }

            }
            
        }
    }
}
