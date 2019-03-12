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

    [Table("Reservations")]
    public class Reservation
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name;
        [DisplayName("Phone Number")]
        public string Phone;
        public string SeatIds;
    }

    public class ReserveData
    {
        public Room Room;
        public List<Seat> Seats;
    }
}
