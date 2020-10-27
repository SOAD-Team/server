﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Language
    {
        [Key]
        public int IdLanguage { get; set; }
        [Required]
        [StringLength(15)]
        public string Name { get; set; }
    }
}