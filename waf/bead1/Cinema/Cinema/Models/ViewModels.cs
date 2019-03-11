using System;
using System.Collections.Generic;
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

    public class ReserveVm
    {
        public Show SelectedShow;
        public Room ShowsRoom;
        public List<Seat> ShowSeats;
    }
}
