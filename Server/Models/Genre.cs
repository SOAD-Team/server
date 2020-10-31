﻿using System;
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

        public Genre(int idGenre, string name)
        {
            MovieDataGenre = new HashSet<MovieDataGenre>();
            IdGenre = idGenre;
            Name = name;
        }

        [Key]
        public int IdGenre { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("IdGenreNavigation")]
        public virtual ICollection<MovieDataGenre> MovieDataGenre { get; set; }
    }
}
