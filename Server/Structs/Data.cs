using AutoMapper;
using Server.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.Structs
{
    public struct Data
    {
        public static Data NullData { get => new Data(null, null, null, null); }
        public MovieData MData { get; set; }
        public Resources.KeyValuePair[] Genres { get; set; }
        public Resources.KeyValuePair[] Languages { get; set; }
        public Resources.KeyValuePair[] Styles { get; set; }

        public Data(MovieData mData, Resources.KeyValuePair[] genres, Resources.KeyValuePair[] languages, Resources.KeyValuePair[] styles)
        {
            this.MData = mData;
            this.Genres = genres;
            this.Languages = languages;
            this.Styles = styles;
        }
    }
}
