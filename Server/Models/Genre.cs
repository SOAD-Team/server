using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Genre
    {
        [Key]
        public int IdGenre { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}
