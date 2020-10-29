using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class User
    {
        public User()
        {
            Movie = new HashSet<Movie>();
        }

        [Key]
        public int IdUser { get; set; }
        [Required]
        [StringLength(89)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        public bool? IsActive { get; set; }

        [InverseProperty("IdUserNavigation")]
        public virtual ICollection<Movie> Movie { get; set; }
    }
}
