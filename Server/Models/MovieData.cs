using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class MovieData
    {
        public MovieData()
        {
            MovieDataGenre = new HashSet<MovieDataGenre>();
            MovieDataLanguage = new HashSet<MovieDataLanguage>();
        }

        [Key]
        public int IdMovieData { get; set; }
        public int IdMovie { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RegisterDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public int Year { get; set; }
        public bool PlatFav { get; set; }
        [Required]
        [Column(TypeName = "text")]
        public string ImageMongoId { get; set; }
        public int IdStyle { get; set; }
        public byte? MetaScore { get; set; }
        [Column("IMDB")]
        public byte? Imdb { get; set; }

        [ForeignKey(nameof(IdMovie))]
        [InverseProperty(nameof(Movie.MovieData))]
        public virtual Movie IdMovieNavigation { get; set; }
        [ForeignKey(nameof(IdStyle))]
        [InverseProperty(nameof(Style.MovieData))]
        public virtual Style IdStyleNavigation { get; set; }
        [InverseProperty("IdMovieDataNavigation")]
        public virtual ICollection<MovieDataGenre> MovieDataGenre { get; set; }
        [InverseProperty("IdMovieDataNavigation")]
        public virtual ICollection<MovieDataLanguage> MovieDataLanguage { get; set; }

        public DTOs.MovieData MapToPresentationModel(int idUser, Genre[] genres, Language[] languages, Image image, Style[] styles)
        {
            return new DTOs.MovieData
            {
                IdUser = idUser,
                IdMovieData = this.IdMovieData,
                IdMovie = this.IdMovie,
                RegisterDate = this.RegisterDate,
                Name = this.Title,
                Year = this.Year,
                Genres = genres,
                Languages = languages,
                PlatFav = this.PlatFav,
                Image = image.MapToPresentationModel(),
                Styles = styles,
                MetaScore = this.MetaScore,
                Imdb = this.Imdb
            };
        }
    }

    
}
