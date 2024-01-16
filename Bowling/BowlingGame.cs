﻿namespace Bowling
{
    public class BowlingGame
    { 
        public int GetScore(List<string> frameScores)
        {
	        var runningTotal = 0;
			var bonus = 0;
			var bonusFrameScore = 0;

			for(var i = 0; i < frameScores.Count; i++)
			{
                runningTotal += CalculateScoreForRound(frameScores[i]);
                
				bonus += CalculateBonus(frameScores[i]);
				if(i == 8) // 8 because 0 based (would be 9 in real terms)
				{
					if (frameScores[i + 1].Contains("x"))
					{
						if (frameScores[i + 1][0].ToString() == "x") 
						{
							var scoresForFinalFrame = frameScores[i + 1].ToCharArray().Select(x => x.ToString()).ToList();
							bonusFrameScore = CalculateBonusScoreForFrame(bonus, scoresForFinalFrame[0], scoresForFinalFrame[1]);
						}
					}
					else
					{
						bonusFrameScore = CalculateBonusScoreForFrame(bonus, frameScores[i + 1], null);
					}
				}
				else if(i == 9)
				{
					var scoresForFinalFrame = frameScores[i].ToCharArray();

					if(scoresForFinalFrame.Length == 3)
					{
						return runningTotal;
					}

					bonusFrameScore = CalculateBonusScoreForFrame(bonus, scoresForFinalFrame[0].ToString(), scoresForFinalFrame[1].ToString());
				}
				else
				{
					bonusFrameScore = CalculateBonusScoreForFrame(bonus, frameScores[i + 1], frameScores[i + 2]);
				}
				runningTotal += bonusFrameScore;
				bonus = 0;
				bonusFrameScore = 0;
			}
            return runningTotal;
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

        public int CalculateBonus(string score)
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
