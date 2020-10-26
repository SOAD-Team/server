using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Review
    {
        [Key]
        public int IdReview { get; set; }
        public int? IdMovie { get; set; }
        public byte? Score { get; set; }
        [Column(TypeName = "text")]
        public string Comment { get; set; }
    }
}
