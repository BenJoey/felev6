using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cinema.Persistence
{
    public class Poster
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int MovieRefId { get; set; }
        [ForeignKey("MovieRefId")]
        public Movie Movie { get; set; }

        public byte[] Image { get; set; }
    }
}
