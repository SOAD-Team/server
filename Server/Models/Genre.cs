using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Genre
    {
        public Genre()
        {
            MovieDataGenre = new HashSet<MovieDataGenre>();
        }

        public Genre(string name)
        {
            MovieDataGenre = new HashSet<MovieDataGenre>();
            Name = name;
        }

        [Key]
        public int IdGenre { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("IdGenreNavigation")]
        public virtual ICollection<MovieDataGenre> MovieDataGenre { get; set; }

        public static Genre Empty { get => empty(); }

        private static Genre empty()
        {
            Genre value = new Genre();
            const string name = "";
            value.Name = name;
            return value;
        }
    }
}
