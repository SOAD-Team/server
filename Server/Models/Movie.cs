﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public partial class Movie
    {
        [Key]
        public int IdMovie { get; set; }
        public int? IdUser { get; set; }
    }
}
