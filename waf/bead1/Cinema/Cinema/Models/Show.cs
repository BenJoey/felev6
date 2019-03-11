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
        [ForeignKey("Movies")]
        public Movie MovieOnAir { get; set; }
        public DateTime StartTime { get; set; }
        public Room Room { get; set; }
    }
}
