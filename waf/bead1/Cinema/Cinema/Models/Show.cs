using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    [Table("Shows")]
    public class Show
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieRefId { get; set; }
        [ForeignKey("MovieRefId")]
        public Movie Movie { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        public int RoomRefId { get; set; }
        [ForeignKey("RoomRefId")]
        public Room Room { get; set; }

        public ICollection<Seat> Seats { get; set; }
    }
}
