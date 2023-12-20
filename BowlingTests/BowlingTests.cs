using Bowling;

namespace BowlingTests
{
    public class BowlingTests
    {

        [Test]
        public void GivenBowlingAlley_WhenPerfectGame_PlayerGets300Points()
        {
            var bowlingGame = new BowlingGame();
            var score = bowlingGame.GetScore();
            Assert.That(score, Is.EqualTo(300));
        }

        [TestCase("x")]
        [TestCase("X")]
        public void GivenBowlingAlley_WhenPlayerGetAStrike_Return10(string score)
        {
            var bowlingGame = new BowlingGame();
            var roundScore = bowlingGame.CalculateScoreForRound(score);
            Assert.That(roundScore, Is.EqualTo(10));
        }
    }
}
