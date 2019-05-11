using System;

namespace Cinema.Persistence.DTOs
{
    public class SeatDto
    {
        public Int32 Id { get; set; }
        public Int32 Row { get; set; }
        public Int32 Col { get; set; }
        public String State { get; set; }
        public String NameReserved { get; set; }

        public String PhoneNum { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is SeatDto dto) && Id == dto.Id;
        }
    }
}
