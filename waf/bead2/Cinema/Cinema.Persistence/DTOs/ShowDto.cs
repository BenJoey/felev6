﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class ShowDto
    {
        [Required]
        public int movieId { get; set; }

        [Required]
        public int roomId { get; set; }

        [Required]
        public String StartTime { get; set; }
    }
}
