using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class UserPoints
    {
        public int Imdb{get; set;}
        public int MetaScore{get; set;}
        public int Community{get; set;}
        public int PlatFav{get; set;}
        public int Popularity { get; set; }
        public Models.Genre Genre { get; set; }

        public static UserPoints Empty { get =>
                new UserPoints
                {
                    Imdb = 0,
                    MetaScore = 0,
                    Community = 0,
                    PlatFav = 0,
                    Popularity = 0,
                    Genre = Models.Genre.Empty
                };
        }
    }
}
