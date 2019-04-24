using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public enum State { Free, Reserved, Sold }

namespace Cinema.Persistence
{
    [Table("Seats")]
    public class Seat
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ShowRefId { get; set; }
        [ForeignKey("ShowRefId")]
        public Show Show { get; set; }

        [ForeignKey("Rooms")]
        public Room Room { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
        public State State { get; set; }
        public string NameReserved { get; set; }

        [Phone()]
        public string PhoneNum { get; set; }
    }
}