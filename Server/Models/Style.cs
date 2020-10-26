using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
