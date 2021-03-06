﻿namespace Server.Resources
{
    public class UserPoints
    {
        public int Imdb{get; set;}
        public int MetaScore{get; set;}
        public int Community{get; set;}
        public int PlatFav{get; set;}
        public int Popularity { get; set; }
        public KeyValuePair Genre { get; set; }
        public static UserPoints Empty { get =>
                new UserPoints
                {
                    Imdb = 0,
                    MetaScore = 0,
                    Community = 0,
                    PlatFav = 0,
                    Popularity = 0,
                    Genre = KeyValuePair.Empty
                };
        }
    }
}
