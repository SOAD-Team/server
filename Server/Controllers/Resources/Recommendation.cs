using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Resources
{
    public class Recommendation
    {
        public Recommendation(Movie movie, int score)
        {
            Movie = movie;
            Score = score;
        }

        public Movie Movie { get; set; }
        public int Score { get; set; }

    }
}
