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
			var bonus = 0;
			var bonusFrameScore = 0;

			for(var i = 0; i < frameScores.Count; i++)
			{
				// we'll be able to identify each round by its index, e.g. frameScore[i],
				runningTotal += CalculateScoreForRound(frameScores[i]);
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
						bonusFrameScore = CalculateBonusScoreForFrame(bonus, scoresForFinalFrame[0], scoresForFinalFrame[1]);
					}
				}
				else
				{
					bonusFrameScore = CalculateBonusScoreForFrame(bonus, frameScores[i + 1], frameScores[i + 2]);
					bonus = 0;
				}
				runningTotal += bonusFrameScore;
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
					else
					{
						if (nextFrame.Contains("/"))
						{
							return 10;
						}
						else
						{
							bonusScore = CalculateScoreForRound(nextFrameScoreFirstThrow); 
							bonusScore += CalculateScoreForRound(nextFrame[1].ToString()); 
							return bonusScore;
						}
					}
				default:
					return 0;
			}
            
			
            // todo if bonus score of 2, then either take both scores from nextFrame
            // or potentially take the first of nextNextFrame if they get a strike
        }

        public int CalculateScoreForRound(string? score)
        {
            switch (score?.ToLower())
            {
                case "x":
                    return 10; //Plus some bonus stuff
				case "/":
					return 10; //maybe more bonus stuff
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
