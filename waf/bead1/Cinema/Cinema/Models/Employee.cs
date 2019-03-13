using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Models
{
    [Table("Employees")]
    public class Employee
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }

        // TODO: Hash the passwords
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
