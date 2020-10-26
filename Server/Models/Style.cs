using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class Style
    {
        [Key]
        public int IdStyle { get; set; }
        [StringLength(15)]
        public string Name { get; set; }
    }
}
