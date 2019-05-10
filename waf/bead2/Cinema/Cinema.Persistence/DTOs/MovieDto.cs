using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public TimeSpan Length { get; set; }

        [DataType(DataType.MultilineText)]
        public String Description { get; set; }

        public String Director { get; set; }
    }
}
