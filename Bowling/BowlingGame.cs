using System.Linq;

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
				if (frameScores[i].ToLower() == "x" || (frameScores[i].Length == 2 && frameScores[i].Contains("/")))
				{
					runningTotal += CalculateScoreForRound(frameScores[i]);
				}
				else
				{
					var throwScores = frameScores[i].ToCharArray();
					foreach (var score in throwScores)
					{
						if (throwScores[2] == '/')
						{
							//ignore first score
							//second is 10
							//add third
						}
						runningTotal += CalculateScoreForRound(score.ToString());
					}

					for(int j = 0; j < throwScores.Length; j++)
					{
						if (throwScores[1] == '/')
						{
							if(j == 0)
							{
								break;
							}
							runningTotal += CalculateScoreForRound(throwScores[j].ToString());
						}
						else
						{
							runningTotal += CalculateScoreForRound(throwScores[j].ToString());
						}
					}
				}

				bonus += CalculateBonus(frameScores[i]);
				if(i == 8) // 8 because 0 based (would be 9 in real terms)
				{
                    //if it contains a x or / then do something special, otherwise that's all that g
                    if (frameScores[i + 1].Contains("x"))
					{
						if (frameScores[i + 1][0].ToString() == "x") 
						{
							var scoresForFinalFrame = frameScores[i + 1].ToCharArray().Select(x => x.ToString()).ToList();
								//.Split("x");
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
					//ToLower doesn't work here and it's splitting XXX into a list of 4 ""

					if(scoresForFinalFrame.Length == 3)
					{
						return runningTotal; // Thinking we might not need to do any bonus stuff for final frame
						bonusFrameScore = CalculateBonusScoreForFrame(bonus, scoresForFinalFrame[0].ToString(), scoresForFinalFrame[1].ToString());
						if (scoresForFinalFrame[2].Equals('x') || scoresForFinalFrame[2].Equals('/'))
						{
							bonusFrameScore += 10;
						}
						else if (scoresForFinalFrame[2].Equals('-'))
						{
							bonusFrameScore += 0;
						}
						else
						{
							bonusFrameScore += int.Parse(scoresForFinalFrame[2].ToString());
						}
					}
					else
					{
						bonusFrameScore = CalculateBonusScoreForFrame(bonus, scoresForFinalFrame[0].ToString(), scoresForFinalFrame[1].ToString());
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
					nextFrameScoreFirstThrow = nextFrame[0].ToString();
					return CalculateScoreForRound(nextFrameScoreFirstThrow);
				case 2:
					nextFrameScoreFirstThrow = nextFrame[0].ToString();
					if(nextFrameScoreFirstThrow == "x")
					{
						bonusScore = CalculateScoreForRound(nextFrameScoreFirstThrow); //10
						bonusScore += CalculateScoreForRound(nextNextFrame?[0].ToString());//5
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
            switch (score)
            {
                case "x":
                    return 10;
				case string spare when spare.Contains ("/"):
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
			if (score == "x")
			{
				return 2;
			}
			else if (score.Contains("/"))
			{
				return 1;
			}
			else
				return 0;
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
