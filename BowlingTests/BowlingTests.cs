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
    }
}
