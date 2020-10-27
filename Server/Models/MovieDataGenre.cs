using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class MovieDataGenre
    {
        public int IdMovieData { get; set; }
        public int IdGenre { get; set; }

        [ForeignKey(nameof(IdGenre))]
        public virtual Genre IdGenreNavigation { get; set; }
        [ForeignKey(nameof(IdMovieData))]
        public virtual MovieData IdMovieDataNavigation { get; set; }
    }
}
