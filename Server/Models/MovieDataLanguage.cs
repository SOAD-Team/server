using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class MovieDataLanguage
    {
        [Key]
        public int IdMovieDataLanguage { get; set; }
        public int IdMovieData { get; set; }
        public int IdLanguage { get; set; }

        [ForeignKey(nameof(IdLanguage))]
        [InverseProperty(nameof(Language.MovieDataLanguage))]
        public virtual Language IdLanguageNavigation { get; set; }
        [ForeignKey(nameof(IdMovieData))]
        [InverseProperty(nameof(MovieData.MovieDataLanguage))]
        public virtual MovieData IdMovieDataNavigation { get; set; }
    }
}
