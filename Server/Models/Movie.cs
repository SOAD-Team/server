using System;
using Server.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Movie")]
    public partial class Movie
    {
        public Movie()
        {
        }

        public Movie(string name, string director, DateTime year, Genre genre, Language language, bool isFavorite, byte qualification, string imageId, byte? iMDB, Style style, byte? metaScore, byte popularity)
        {
            Name = name;
            Director = director;
            Year = year;
            Genre = genre;
            Language = language;
            IsFavorite = isFavorite;
            Qualification = qualification;
            ImageId = imageId;
            IMDB = iMDB;
            Style = style;
            MetaScore = metaScore;
            Popularity = popularity;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdMovie {get; set;}
        [Column("Name")]
        [StringLength(50)]
        public string Name {get; set;}
        [Column("Director")]
        [StringLength(30)]
        public string Director {get; set;}
        [Column("Year", TypeName = "date")]
        public DateTime Year {get; set;}
        [Column("Genre")]
        public Genre Genre {get; set;}
        [Column("Language")]
        public Language Language {get; set;}
        [Column("IsFavorite")]
        public bool IsFavorite {get; set;}
        [Column("Qualification")]
        public byte Qualification {get; set;}
        [Column("ImageId")]
        public string ImageId {get; set;}
        [Column("IMDB")]
        public byte? IMDB {get; set;}
        [Column("Style")]
        public Style Style {get; set;}
        [Column("MetaScore")]
        public byte? MetaScore {get; set;}
        [Column("Popurarity")]
        public byte Popularity {get; set;}
    }
}