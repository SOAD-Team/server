using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Style
    {
        public Style()
        {
            MovieData = new HashSet<MovieData>();
        }

        [Key]
        public int IdStyle { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        [InverseProperty("IdStyleNavigation")]
        public virtual ICollection<MovieData> MovieData { get; set; }
    }
}
