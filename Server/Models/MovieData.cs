using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class MovieData
    {
        [Key]
        public int IdMovieData { get; set; }
        public int? IdMovieHistory { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RegisterDate { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(4)]
        public string Year { get; set; }
        public int? IdGenre { get; set; }
        public int? IdLanguage { get; set; }
        public bool? PlatFav { get; set; }
        [Column(TypeName = "text")]
        public string ImageMongoId { get; set; }
        public byte? IdStyle { get; set; }
        public byte? MetaScore { get; set; }
        [Column("IMDB")]
        public byte? Imdb { get; set; }
    }
}
