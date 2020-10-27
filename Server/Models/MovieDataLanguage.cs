using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class MovieDataLanguage
    {
        public int IdMovieData { get; set; }
        public int IdLanguage { get; set; }

        [ForeignKey(nameof(IdLanguage))]
        public virtual Language IdLanguageNavigation { get; set; }
        [ForeignKey(nameof(IdMovieData))]
        public virtual MovieData IdMovieDataNavigation { get; set; }
    }
}
