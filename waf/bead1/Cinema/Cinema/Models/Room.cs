using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Room Name")]
        public string RoomName { get; set; }
        public int NumOfRows { get; set; }
        public int NumOfCols { get; set; }

        public ICollection<Show> Shows { get; set; }
    }
}

