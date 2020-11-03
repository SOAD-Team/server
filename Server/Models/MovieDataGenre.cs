using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class MovieDataGenre
    {
        public MovieDataGenre(int idMovieData, int idGenre)
        {
            IdMovieData = idMovieData;
            IdGenre = idGenre;
        }
        [Key]
        public int IdMovieDataGenre { get; set; }
        public int IdMovieData { get; set; }
        public int IdGenre { get; set; }

        [ForeignKey(nameof(IdGenre))]
        [InverseProperty(nameof(Genre.MovieDataGenre))]
        public virtual Genre IdGenreNavigation { get; set; }
        [ForeignKey(nameof(IdMovieData))]
        [InverseProperty(nameof(MovieData.MovieDataGenre))]
        public virtual MovieData IdMovieDataNavigation { get; set; }
    }
}
