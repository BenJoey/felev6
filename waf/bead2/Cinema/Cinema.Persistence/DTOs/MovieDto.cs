﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class MovieDto
    {
        public Int32 Id { get; set; }

        [Required]
        public String Title { get; set; }

        public String Length { get; set; }

        public String Description { get; set; }

        public String Director { get; set; }

        public Byte[] Poster { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is MovieDto dto) && Id == dto.Id;
        }
    }
}
