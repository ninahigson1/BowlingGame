namespace Bowling
{
    public class BowlingGame
    { 
        public int GetScore(List<string> frameScores)
        {
	        var runningTotal = 0;
			var bonusThrowsToCalculate = 0;
            
			for(var i = 0; i < frameScores.Count; i++)
			{
                bonusThrowsToCalculate += CalculateBonusThrows(frameScores[i]);
                runningTotal += CalculateScoreForRound(frameScores[i]);
				runningTotal += CalculateBonusScoreForRound(frameScores, i, bonusThrowsToCalculate);
				bonusThrowsToCalculate = 0;
			}
            return runningTotal;
        }

        private int CalculateBonusScoreForRound(List<string> frameScores, int i, int bonusThrowsToCalculate)
        {
            int bonusFrameScore = 0;
            if (bonusThrowsToCalculate == 0)
            {
                return 0;
            }
            if (i == 8) // 8 because 0 based (would be 9 in real terms)
            {
                if (frameScores[i + 1].Contains("x"))
                {
                    if (frameScores[i + 1][0].ToString() == "x")
                    {
                        var scoresForFinalFrame = frameScores[i + 1].ToCharArray().Select(x => x.ToString()).ToList();
                        bonusFrameScore = CalculateBonusScoreForFrame(bonusThrowsToCalculate, scoresForFinalFrame[0],
                            scoresForFinalFrame[1]);
                    }
                }
                else
                {
                    bonusFrameScore = CalculateBonusScoreForFrame(bonusThrowsToCalculate, frameScores[i + 1], null);
                }
            }
            else if (i == 9)
            {
                var scoresForFinalFrame = frameScores[i].ToCharArray();
                var score = 0;

                if (scoresForFinalFrame.Length == 3)
                {
                    return score;
                }

                bonusFrameScore = CalculateBonusScoreForFrame(bonusThrowsToCalculate, scoresForFinalFrame[0].ToString(),
                    scoresForFinalFrame[1].ToString());
            }
            else
            {
                bonusFrameScore = CalculateBonusScoreForFrame(bonusThrowsToCalculate, frameScores[i + 1], frameScores[i + 2]);
            }

            return bonusFrameScore;
        }

        private int CalculateBonusScoreForFrame(int bonus, string nextFrame, string? nextNextFrame)
        {
	        string nextFrameScoreFirstThrow = "";
			switch(bonus)
			{
				case 1:
					nextFrameScoreFirstThrow = nextFrame[0].ToString();
					return CalculateScoreForRound(nextFrameScoreFirstThrow);
				case 2:
					nextFrameScoreFirstThrow = nextFrame[0].ToString();
					var bonusScore = 0;
					if(nextFrameScoreFirstThrow == "x")
					{
						bonusScore = CalculateScoreForRound(nextFrameScoreFirstThrow);
						bonusScore += CalculateScoreForRound(nextNextFrame?[0].ToString());
						return bonusScore;
					}

					if (nextFrame.Contains('/'))
					{
						return 10;
					}

					bonusScore = CalculateScoreForRound(nextFrameScoreFirstThrow); 
					bonusScore += CalculateScoreForRound(nextFrame[1].ToString()); 
					return bonusScore;
				default:
					return 0;
			}
			
        }

        public int CalculateScoreForRound(string? round)
        {
	        int roundScore = 0;
	        if (round == null)
	        {
		        return roundScore;
	        }

	        var throwScores = round.ToCharArray();

	        for (int j = 0; j < throwScores.Length; j++)
	        {
		        if (throwScores.Length > 1 && throwScores[1] == '/')
		        {
			        if (j == 0)
			        {
				        continue; //essentially ignore first score as we handle a spare separately.
			        }

			        roundScore += CalculateScoreForThrow(throwScores[j].ToString());
		        }
		        else
		        {
			        roundScore += CalculateScoreForThrow(throwScores[j].ToString());
		        }
	        }
	        return roundScore;
		}
        private static int CalculateScoreForThrow(string roundScore)
        {
            switch (roundScore)
            {
                case "x":
                case "/":
	                return 10;
                case "-":
                case null:
                    return 0;
                default:
                    return int.Parse(roundScore);
            }
        }
        public int CalculateBonusThrows(string score)
		{
			if (score == "x")
			{
				return 2;
			}

			return score.Contains("/") ? 1 : 0;
		}
        public List<string> SplitScoresIntoFrames(string scores)
		{
			scores = scores.ToLowerInvariant();
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
