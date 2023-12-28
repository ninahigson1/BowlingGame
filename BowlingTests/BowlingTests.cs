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

        [TestCase("X X X X X X X X X X X X")]
        [TestCase("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-")]
        [TestCase("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5")]
		public void Given_TenFramesOfStrikes_ThenScoreIsCalculated(string scores)
        {
	        var bowlingGame = new BowlingGame();
            var listOfScores = bowlingGame.SplitScoresIntoFrames(scores);
            Assert.That(listOfScores.Count, Is.EqualTo(10));
            //a method that splits the scores based on space but then for the 10th frame joins 3?

        }
    }
}
