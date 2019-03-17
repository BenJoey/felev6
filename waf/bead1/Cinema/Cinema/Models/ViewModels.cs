using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class MovieIndexViewModel
    {
        public List<Movie> Films;
        public List<Show> ShowTimes;
        public List<Room> Rooms;
    }

    public class MovieDetailsViewModel
    {
        public Movie Film;
        public List<Show> ShowTimes;
        public List<Room> Rooms;
    }
    
    public class Reservation
    {
        [Required(ErrorMessage = "You must provide a name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Phone(),Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string SeatIds { get; set; }

        public int ShowId { get; set; }
        public Room Room { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
