using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    public class Room
    {
        
        public String Name { get; set; }
        public Int32 NumOfRows { get; set; }
        public Int32 NumOfCols { get; set; }
    }
}

