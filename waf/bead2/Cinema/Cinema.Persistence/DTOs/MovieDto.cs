using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class MovieDto
    {
        public Int32 Id { get; set; }

        public String Title { get; set; }

        public String Length { get; set; }

        public String Description { get; set; }

        public String Director { get; set; }

        public ImageDto Poster { get; set; }
    }
}
