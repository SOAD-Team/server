using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class MovieDataGenre
    {
        [Key]
        public int IdMovieDataGenre { get; set; }
        public int IdMovieData { get; set; }
        public int IdGenre { get; set; }

        [ForeignKey(nameof(IdGenre))]
        [InverseProperty(nameof(Genre.MovieDataGenre))]
        public virtual Genre IdGenreNavigation { get; set; }
        [ForeignKey(nameof(IdMovieData))]
        [InverseProperty(nameof(MovieData.MovieDataGenre))]
        public virtual MovieData IdMovieDataNavigation { get; set; }
    }
}
