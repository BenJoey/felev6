using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class MovieVm
    {
        public List<Movie> Films;
        public List<Show> ShowTimes;
        public List<Room> Rooms;
    }

    public class MovieDetailsVm
    {
        public Movie Film;
        public List<Show> ShowTimes;
        public List<Room> Rooms;
    }
    
    public class Reservation
    {
        public string Name { get; set; }
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
        public string SeatIds { get; set; }
    }

    public class ReserveData
    {
        public Room Room { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
