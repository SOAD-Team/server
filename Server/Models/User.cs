using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public partial class User
    {
        [Key]
        public int IdUser { get; set; }
        [StringLength(89)]
        public string Email { get; set; }
        [StringLength(30)]
        public string Password { get; set; }
    }
}
