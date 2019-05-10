using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class ReservationDto
    {
        public String Name { get; set; }
        public String PhoneNum { get; set; }
        public IEnumerable<Int32> SelectedSeats { get; set; }
    }
}
