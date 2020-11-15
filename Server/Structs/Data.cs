using Server.Models;

namespace Server.Structs
{
    public struct Data
    {
        public static Data NullData { get => new Data(null, null, null, null); }
        public MovieData MData { get; }
        public Genre[] Genres { get; }
        public Language[] Languages { get; }
        public Style[] Styles { get; }

        public Data(MovieData mData, Genre[] genres, Language[] languages, Style[] styles)
        {
            this.MData = mData;
            this.Genres = genres;
            this.Languages = languages;
            this.Styles = styles;
        }
    }
}
