using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Models
{
    [Table("Movies")]
    public class Movie
    {
        public Movie()
        {
            Modified = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Title { get; set; }

        public string PosterPath { get; set; }

        public string Director { get; set; }

        public int Length { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Modified { get; set; }

        public ICollection<Show> Shows { get; set; }
    }
}