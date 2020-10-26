using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class Movie
    {
        [Key]
        public int IdMovie { get; set; }
        public int? IdUser { get; set; }
    }
}
