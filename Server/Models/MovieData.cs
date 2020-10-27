using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class MovieData
    {
        [Key]
        public int IdMovieData { get; set; }
        public int IdMovie { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime RegisterDate { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(4)]
        public string Year { get; set; }
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
    }
}
