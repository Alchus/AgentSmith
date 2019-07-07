using System.Linq;
using AgentSmith.Agents;
using AgentSmith.Framework;
using AgentSmith.Games.TicTacToe;
using AgentSmithFramework.Agents;
using NUnit.Framework;

namespace AgentSmithTests.TicTacToe.Tests
{
    [TestFixture]
    class DFSTests
    {
        [Test]
        public void DfsAgentDrawsItself()
        {
            Agent X = new MiniMaxAgent("MinMaxAgent1");
            Agent O = new MiniMaxAgent("MinMaxAgent2");

            var game = new AgentSmith.Games.TicTacToe.TicTacToe(new[] { X, O });
            game.Run();

            Assert.That(game.IsFinished);
            Assert.AreEqual(0.5, game.GetScore(X));
            Assert.AreEqual(0.5, game.GetScore(O));
            Assert.IsEmpty(((TicTacToeState)game.State).board.Where(x => x.Equals('-')));
        }

        [Test]
        public void DfsNeverLosesToRandom_PlaySecond()
        {
            for (int i = 0; i < 200; i++)
            {
                Agent X = new RandomPlayAgent("Random1");
                Agent O = new MiniMaxAgent("MinMaxAgent");

                var game = new AgentSmith.Games.TicTacToe.TicTacToe(new[] { X, O });
                game.Run();

                Assert.That(game.IsFinished);
                Assert.LessOrEqual(game.GetScore(X), 0.5);
                Assert.GreaterOrEqual(game.GetScore(O),0.5);
            }
            
        }
    }
}