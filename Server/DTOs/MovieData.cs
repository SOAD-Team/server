using Server.Models;
using System;

namespace Server.DTOs
{
    public class MovieData
    {
        public static MovieData Empty { get => empty(); }

        private static MovieData empty()
        {
            MovieData data = new MovieData();
            data.IdUser = User.Empty.IdUser;
            data.RegisterDate = new DateTime();
            data.Name = "";
            data.Year = 0;
            data.Genres = new Models.Genre[1] {Genre.Empty};
            data.Languages = new Models.Language[1] {Language.Empty};
            data.PlatFav = false;
            data.Styles = new Models.Style[1]{ Models.Style.Empty };
            data.Image = Image.Empty;
            data.Director = "";

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
        public string Director { get; set; }

        public MovieData()
        {

        }

        public MovieData(Movie movie, Models.MovieData data, Genre[] genres, Language[] languages, Style[] styles, Models.Image image)
        {
            this.IdMovie = movie.IdMovie;
            this.IdUser = movie.IdUser;
            this.IdMovieData = data.IdMovieData;
            this.RegisterDate = data.RegisterDate;
            this.Name = data.Title;
            this.Year = data.Year;
            this.Genres = genres;
            this.Languages = languages;
            this.PlatFav = data.PlatFav;
            this.Styles = styles;
            this.Image = image.MapToPresentationModel();
        }

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
                IdStyle = this.Styles[0].IdStyle,
                Director = this.Director
            };
            return data;
        }
    }
}
