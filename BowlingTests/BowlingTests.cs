using Bowling;

namespace BowlingTests
{
    public class BowlingTests
    {
		[TestCase("X X X X X X X X X X X X")]
		public void GivenBowlingAlley_WhenPerfectGame_PlayerGets300Points(string scores)
        {
            var bowlingGame = new BowlingGame();
            var listOfScores = bowlingGame.SplitScoresIntoFrames(scores);
            var score = bowlingGame.GetScore(listOfScores);
            Assert.That(score, Is.EqualTo(300));
        }

		[TestCase("9- 9- 9- 9- 9- 9- 9- 9- 9- 9-", 90)]
		[TestCase("52 45 12 13 14 15 15 22 23 5-", 54)]
		public void GivenBowlingAlley_WhenNoStrikesOrSpares_PlayerGetsCorrectPoints(string scores, int expectedScore)
		{
			var bowlingGame = new BowlingGame();
			var listOfScores = bowlingGame.SplitScoresIntoFrames(scores);
			var score = bowlingGame.GetScore(listOfScores);
			Assert.That(score, Is.EqualTo(expectedScore));
		}

		[TestCase("x")]
		public void GivenBowlingAlley_WhenPlayerGetAStrike_Return10(string score)
        {
            var bowlingGame = new BowlingGame();
            var roundScore = bowlingGame.CalculateScoreForRound(score);
            Assert.That(roundScore, Is.EqualTo(10));
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
