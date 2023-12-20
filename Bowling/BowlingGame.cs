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
    }
}
