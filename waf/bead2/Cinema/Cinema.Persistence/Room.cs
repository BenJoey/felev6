using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.Persistence
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
    }
}

