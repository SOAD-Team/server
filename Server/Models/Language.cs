using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class Language
    {
        [Key]
        public int IdLanguage { get; set; }
        [StringLength(15)]
        public string Name { get; set; }
    }
}
