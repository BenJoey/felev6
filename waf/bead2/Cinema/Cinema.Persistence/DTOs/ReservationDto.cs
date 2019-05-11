using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class ReservationDto
    {
        [Required]
        public String Name { get; set; }

        [Required]
        public String PhoneNum { get; set; }

        public IEnumerable<Int32> SelectedSeats { get; set; }
    }
}
