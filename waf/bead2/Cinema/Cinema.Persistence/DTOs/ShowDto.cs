using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Persistence.DTOs
{
    public class ShowDto
    {
        [Required]
        public Int32 movieId { get; set; }

        [Required]
        public Int32 roomId { get; set; }

        [Required]
        public String StartTime { get; set; }
    }
}
