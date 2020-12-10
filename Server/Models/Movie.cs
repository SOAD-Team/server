using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Movie
    {
        public Movie()
        {
            MovieData = new HashSet<MovieData>();
            Review = new HashSet<Review>();
        }

        public Movie(int idUser)
        {
            this.IdUser = idUser;
            MovieData = new HashSet<MovieData>();
            Review = new HashSet<Review>();
        }

        [Key]
        public int IdMovie { get; set; }
        public int IdUser { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(IdUser))]
        [InverseProperty(nameof(User.Movie))]
        public virtual User IdUserNavigation { get; set; }
        [InverseProperty("IdMovieNavigation")]
        public virtual ICollection<MovieData> MovieData { get; set; }
        [InverseProperty("IdMovieNavigation")]
        public virtual ICollection<Review> Review { get; set; }
        public static Movie Empty { get => new Movie(User.Empty.IdUser); }
    }
}
