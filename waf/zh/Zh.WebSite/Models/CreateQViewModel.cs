using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zh.WebSite.Models
{
    public class CreateQViewModel
    {
        [Required]
        public string QuestionText { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime QuestionDueTime { get; set; }

        [Required]
        public string Answer1 { get; set; }
        [Required]
        public string Answer2 { get; set; }
        [Required]
        public string Answer3 { get; set; }
        [Required]
        public string Answer4 { get; set; }
    }
}
