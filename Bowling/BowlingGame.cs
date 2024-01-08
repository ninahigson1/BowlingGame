namespace Bowling
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
				// we'll be able to identify each round by its index, e.g. frameScore[i],
				if (frameScores[i].ToLower() == "x" || frameScores[i].Contains("/"))
				{
					runningTotal += CalculateScoreForRound(frameScores[i]);
				}
				else
				{
					var throwScores = frameScores[i].ToCharArray();
					foreach (var score in throwScores)
					{
						runningTotal += CalculateScoreForRound(score.ToString());
					}
				}

				bonus += CalculateBonus(frameScores[i]);
				if(i == 8) // 8 because 0 based (would be 9 in real terms)
				{
                    //if it contains a x or / then do something special, otherwise that's all that g
                    if (frameScores[i + 1].Contains("x") || frameScores[i + 1].Contains("/"))
					{
						if (frameScores[i + 1][0].ToString().ToLower() == "x")
						{
							var scoresForFinalFrame = frameScores[i+1].ToLower().Split('x');
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
					var scoresForFinalFrame = frameScores[i].ToLower().Split('x'); 
					//ToLower doesn't work here and it's splitting XXX into a list of 4 ""

					if(scoresForFinalFrame.Length == 3)
					{
						bonusFrameScore = CalculateBonusScoreForFrame(bonus, scoresForFinalFrame[0], scoresForFinalFrame[1]);
						if (scoresForFinalFrame[2].ToLower().Equals("x") || scoresForFinalFrame[2].ToLower().Equals("/"))
						{
							bonusFrameScore += 10;
						}
						else if (scoresForFinalFrame[2].Equals("-"))
						{
							bonusFrameScore += 0;
						}
						else
						{
							bonusFrameScore += int.Parse(scoresForFinalFrame[2]);
						}
					}
					else
					{
						var finalRoundScoresTwoThrowsOnly = scoresForFinalFrame[0].ToCharArray();
						bonusFrameScore = CalculateBonusScoreForFrame(bonus, finalRoundScoresTwoThrowsOnly[0].ToString(), finalRoundScoresTwoThrowsOnly[1].ToString());
					}
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
            var bonusScore = 0;
            var nextFrameScoreFirstThrow = "";
			switch(bonus)
			{
				case 1:
					nextFrameScoreFirstThrow = nextFrame[0].ToString().ToLower();
					return CalculateScoreForRound(nextFrameScoreFirstThrow);
				case 2:
					nextFrameScoreFirstThrow = nextFrame[0].ToString().ToLower();
					if(nextFrameScoreFirstThrow == "x")
					{
						bonusScore = CalculateScoreForRound(nextFrameScoreFirstThrow); //10
						bonusScore += CalculateScoreForRound(nextNextFrame?[0].ToString().ToLower());//5
						return bonusScore; //15 
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

        public int CalculateScoreForRound(string? score)
        {
            switch (score?.ToLower())
            {
                case "x":
                    return 10;
				case "/":
					return 10;
				case "-":
				case null:
					return 0;
                default:
                    return int.Parse(score);
            }
        }

		public int CalculateBonus(string score)
		{
			if (score.ToLower() == "x")
			{
				return 2;
			}
			else if (score.ToLower() == "/")
			{
				return 1;
			}
			else
				return 0;
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
