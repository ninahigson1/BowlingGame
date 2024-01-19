using Bowling;

namespace BowlingTests
{
    public class BowlingTests
    {
		[TestCase("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-", 90)]
		[TestCase("52 45 12 13 14 15 15 22 23 5-", 54)]
        [TestCase("X X X X X X X X X X X X", 300)]
        [TestCase("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5", 150)]
        public void GivenBowlingAlley_WhenProvidedAFullGameOfScores_PlayerGetsCorrectPoints(string scores, int expectedScore)
		{
			var bowlingGame = new BowlingGame();
			var listOfScores = bowlingGame.SplitScoresIntoFrames(scores);
			var score = bowlingGame.GetScore(listOfScores);
			Assert.That(score, Is.EqualTo(expectedScore));
		}

		[TestCase("x", 10)]
        [TestCase("5/", 10)]
        [TestCase("5-", 5)]
        [TestCase("--", 0)]
        public void GivenBowlingAlley_WhenPlayerGetAStrike_Return10(string score, int expectedRoundScore)
        {
            var bowlingGame = new BowlingGame();
            var roundScore = bowlingGame.CalculateScoreForRound(score);
            Assert.That(roundScore, Is.EqualTo(expectedRoundScore));
        }

        [TestCase("X X X X X X X X X X X X")]
        [TestCase("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-")]
        [TestCase("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5")]
		public void GivenTenFramesOfScores_ThenScoreIsSplitIntoTenFrames(string scores)
        {
	        var bowlingGame = new BowlingGame();
            var listOfScores = bowlingGame.SplitScoresIntoFrames(scores);
            Assert.That(listOfScores.Count, Is.EqualTo(10));
        }
    }
}
