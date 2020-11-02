using System;

namespace Server.DTOs
{
    public class MovieData
    {
        public static MovieData Empty { get => empty(); }

        private static MovieData empty()
        {
            MovieData data = new MovieData();
            data.IdUser = 0;
            data.RegisterDate = new DateTime();
            data.Name = "";
            data.Year = 0;
            data.Genres = new Models.Genre[0];
            data.Languages = new Models.Language[0];
            data.PlatFav = false;
            data.Styles = new Models.Style[1]{ Models.Style.Empty };
            data.Image = Image.Empty;

            return data;
        }

        public int IdUser { get; set; }
        public int? IdMovieData { get; set; }
        public int? IdMovie { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Models.Genre[] Genres { get; set; }
        public Models.Language[] Languages { get; set; }
        public bool PlatFav { get; set; }
        public Image Image { get; set; }
        public Models.Style[] Styles { get; set; }
        public byte? MetaScore { get; set; }
        public byte? Imdb { get; set; }

        public Models.MovieData MapToModel(int idMovie,string idImage)
        {
            Models.MovieData data = new Models.MovieData
            {
                IdMovie = idMovie,
                RegisterDate = this.RegisterDate,
                Title = this.Name,
                Year = this.Year,
                PlatFav = this.PlatFav,
                ImageMongoId = idImage,
                MetaScore = this.MetaScore,
                Imdb = this.Imdb,
                IdStyle = this.Styles[0].IdStyle
            };
            return data;
        }
    }
}
