using System;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Persistence.DTOs
{
    public class ShowDto
    {
        public Int32 showId { get; set; }

        public Int32 movieId { get; set; }

        public String movieName { get; set; }

        public Int32 roomId { get; set; }

        [Required]
        public String StartTime { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is ShowDto dto) && showId == dto.showId;
        }
    }
}
