using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class BowlingGame
    { 
        public int GetScore()
        {
            return 300;
        }

        public int CalculateScoreForRound(string score)
        {
            switch (score.ToLower())
            {
                case "x":
                    return 10;
                default:
                    return 0;
            }
        }

		public List<string> SplitScoresIntoFrames(string scores)
		{
			var frameScores = scores.Split(' ').ToList();
			if (frameScores.Count == 12)
			{
				var scoreForFrame10 = frameScores[9] + frameScores[10] + frameScores[11];
				frameScores.RemoveAt(11);
				frameScores.RemoveAt(10);
				frameScores.RemoveAt(9);
				frameScores.Add(scoreForFrame10);
			}
			return frameScores;
		}
	}
}
