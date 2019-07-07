using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgentSmith;
using AgentSmith.Agents;
using AgentSmith.Framework;
using NUnit.Framework;

namespace AgentSmith.Games.TicTacToe.Tests
{
    [TestFixture]
    class InteractiveTests
    {
        [Test]
        public void HumanPlayerWins()
        {
            Agent X = new HumanAgent("Human1");
            Agent O = new RandomPlayAgent("RandomAgent");

            TicTacToe game = new TicTacToe(new[]{X, O}); 
            game.Run();

            Assert.That(game.IsFinished);
            Assert.AreEqual(1.0, game.GetScore(X));
            Assert.AreEqual(0.0, game.GetScore(O));
        }

        public void HumanPlayerLoses()
        {
            Agent X = new HumanAgent("Human1");
            Agent O = new RandomPlayAgent("RandomAgent");

            TicTacToe game = new TicTacToe(new[] { X, O });
            game.Run();

            Assert.That(game.IsFinished);
            Assert.AreEqual(0.0, game.GetScore(X));
            Assert.AreEqual(1.0, game.GetScore(O));
        }

        public void HumanDraws()
        {
            Agent X = new HumanAgent("Human1");
            Agent O = new RandomPlayAgent("RandomAgent");

            TicTacToe game = new TicTacToe(new[] { X, O });
            game.Run();

            Assert.That(game.IsFinished);
            Assert.AreEqual(0.5, game.GetScore(X));
            Assert.AreEqual(0.5, game.GetScore(O));
        }
    }
}
