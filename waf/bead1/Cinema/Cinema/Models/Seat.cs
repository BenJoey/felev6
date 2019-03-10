using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum State { Free, Reserved, Sold}

namespace Cinema.Models
{
    public class Seat
    {
        public Show Show { get; set; }
        public Room Room { get; set; }
        public Int32 Row { get; set; }
        public Int32 Col { get; set; }
        public State State { get; set; }
        public String FoglaloNeve { get; set; }
        public Int32 FoglaloSzama { get; set; }
    }
}