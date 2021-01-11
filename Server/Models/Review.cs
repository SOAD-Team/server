using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Review
    {
        [Key]
        public int IdReview { get; set; }
        public int IdMovie { get; set; }
        public byte Score { get; set; }
        [Column(TypeName = "text")]
        public string Comment { get; set; }

        [ForeignKey(nameof(IdMovie))]
        [InverseProperty(nameof(Movie.Review))]
        public virtual Movie IdMovieNavigation { get; set; }
        public static Review Empty { get => new Review(); }

    }
}
