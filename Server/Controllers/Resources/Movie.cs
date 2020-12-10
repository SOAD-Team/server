using Server.Models;
using System;

namespace Server.Resources
{
    public class Movie
    {
        public static Movie Empty { get => empty(); }

        private static Movie empty()
        {
            Movie data = new Movie();
            data.IdUser = User.Empty.IdUser;
            data.RegisterDate = new DateTime();
            data.Name = "";
            data.Year = 0;
            data.Genres = new KeyValuePair[1] { KeyValuePair.Empty };
            data.Languages = new KeyValuePair[1] { KeyValuePair.Empty };
            data.PlatFav = false;
            data.Styles = new KeyValuePair[1]{ KeyValuePair.Empty };
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
        public KeyValuePair[] Genres { get; set; }
        public KeyValuePair[] Languages { get; set; }
        public bool PlatFav { get; set; }
        public Image Image { get; set; }
        public KeyValuePair[] Styles { get; set; }
        public byte? MetaScore { get; set; }
        public byte? Imdb { get; set; }
        public string Director { get; set; }

        public Movie()
        {

        }

        public Movie(int idMovie, int idUser, Models.MovieData data, KeyValuePair[] genres, KeyValuePair[] languages, KeyValuePair[] styles, Models.Image image)
        {
            this.IdMovie = idMovie;
            this.IdUser = idUser;
            this.IdMovieData = data.IdMovieData;
            this.RegisterDate = data.RegisterDate;
            this.Name = data.Title;
            this.Year = data.Year;
            this.Director = data.Director;
            this.Imdb = data.Imdb;
            this.MetaScore = data.MetaScore;
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
                IdStyle = this.Styles[0].Id,
                Director = this.Director
            };
            return data;
        }
    }
}
