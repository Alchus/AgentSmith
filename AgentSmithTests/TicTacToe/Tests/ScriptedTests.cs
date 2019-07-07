using AgentSmith.Agents;
using AgentSmith.Framework;
using AgentSmith.Games.TicTacToe;
using NUnit.Framework;

namespace AgentSmithTests.TicTacToe.Tests
{
    [TestFixture]
    class ScriptedTests
    {
        [Test]
        public void PlayerXWins()
        {
            Agent X = new ScriptedMoveIDAgent("ScriptedAgentX", new[] { 0, 1, 2 });
            Agent O = new ScriptedMoveIDAgent("ScriptedAgentO", new[] { 5, 7 });

            var game = new AgentSmith.Games.TicTacToe.TicTacToe(new[]{X, O}); 
            game.Run();

            Assert.That(game.IsFinished);
            Assert.AreEqual(1.0, game.GetScore(X));
            Assert.AreEqual(0.0, game.GetScore(O));
        }
    }
}
