using System.Collections.Generic;
using Cinema.Persistence;

namespace Cinema.WebSite.Models
{
    public class MovieDetailsViewModel
    {
        public Movie Film;
        public List<Show> ShowTimes;
        public List<Room> Rooms;
    }
}
