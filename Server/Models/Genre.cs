using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class Genre
    {
        [Key]
        public int IdGenre { get; set; }
        [StringLength(15)]
        public string Name { get; set; }
    }
}
