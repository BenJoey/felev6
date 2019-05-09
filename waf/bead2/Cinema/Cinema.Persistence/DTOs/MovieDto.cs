using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class MovieDto
    {
        [Required]
        public String Title { get; set; }

        [Required]
        public TimeSpan Length { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        [Required]
        public String Director { get; set; }
    }
}
