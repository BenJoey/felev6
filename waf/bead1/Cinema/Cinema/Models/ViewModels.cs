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
        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        [Required]
        public string SeatIds { get; set; }
        public int ShowId { get; set; }
        public Room Room { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
