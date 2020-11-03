using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class User
    {
        public static User Empty { get => new User("", "", "", ""); }
        public User()
        {
            Movie = new HashSet<Movie>();
        }

        public User(string email, string password, string name, string lastName)
        {
            Email = email;
            Password = password;
            Name = name;
            LastName = lastName;
        }

        [Key]
        public int IdUser { get; set; }
        [Required]
        [StringLength(89)]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(40)]
        public string LastName { get; set; }

        [InverseProperty("IdUserNavigation")]
        public virtual ICollection<Movie> Movie { get; set; }
    }
}
