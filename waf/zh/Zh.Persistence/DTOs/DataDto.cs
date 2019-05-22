using System;
using System.ComponentModel.DataAnnotations;

namespace Zh.Persistence.DTOs
{
    public class DataDto
    {
        public Int32 Id { get; set; }

        [Required]
        public String Name { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is DataDto dto) && Id == dto.Id;
        }
    }
}
