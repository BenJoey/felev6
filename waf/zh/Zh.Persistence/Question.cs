using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zh.Persistence
{
    public class Question
    {
        public Question()
        {
            
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        public string Subject { get; set; }

        public DateTime DueTime { get; set; }

        public override Boolean Equals(Object obj)
        {
            return (obj is Question data) && Id == data.Id;
        }
    }
}