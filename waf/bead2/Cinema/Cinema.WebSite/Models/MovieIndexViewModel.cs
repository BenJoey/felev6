using System.Collections.Generic;
using Cinema.Persistence;

namespace Cinema.WebSite.Models
{
    public class MovieIndexViewModel
    {
        public List<Movie> Films;
        public List<Show> ShowTimes;
        public List<Room> Rooms;
    }
}
