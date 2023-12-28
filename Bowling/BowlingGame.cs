using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class BowlingGame
    { 
        public int GetScore(List<string> frameScores)
        {
	        var runningTotal = 0;
			// think we would need a way to calculate score per frame
			foreach (var score in frameScores)
	        {
		        if (score.ToLower() == "x")
		        {
			        runningTotal += CalculateScoreForRound(score);
		        }
		        if (score.Contains("/"))
		        {
					//Calculate score for a strike
					runningTotal+= CalculateScoreForRound(score);
		        }
		        else
		        {
			        var throwScores = score.ToCharArray();
			        foreach (var i in throwScores)
			        {
				        runningTotal+= CalculateScoreForRound(i.ToString());
			        }
		        }
	        }
            return runningTotal;
        }

        public int CalculateScoreForRound(string score)
        {
            switch (score.ToLower())
            {
                case "x":
                    return 10; //Plus some bonus stuff
				case "/":
					return 10; //maybe more bonus stuff
				case "-":
					return 0;
                default:
                    return int.Parse(score);
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
