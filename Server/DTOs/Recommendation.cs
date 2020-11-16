using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class Recommendation
    {
        public Recommendation(MovieData movie, int score)
        {
            Movie = movie;
            Score = score;
        }

        public MovieData Movie { get; set; }
        public int Score { get; set; }

    }
}
