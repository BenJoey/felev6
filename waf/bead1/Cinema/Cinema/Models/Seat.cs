using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum State { Free, Reserved, Sold}

namespace Cinema.Models
{
    [Table("Seats")]
    public class Seat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Shows")]
        public Show Show { get; set; }

        [ForeignKey("Rooms")]
        public Room Room { get; set; }
        public Int32 Row { get; set; }
        public Int32 Col { get; set; }
        public State State { get; set; }
        public String NameReserved { get; set; }
        public Int32 PhoneNum { get; set; }
    }
}