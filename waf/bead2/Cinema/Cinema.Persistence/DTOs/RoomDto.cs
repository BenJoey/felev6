using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Persistence.DTOs
{
    public class RoomDto
    {
        public Int32 Id { get; set; }

        public String RoomName { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is RoomDto dto) && Id == dto.Id;
        }
    }
}
