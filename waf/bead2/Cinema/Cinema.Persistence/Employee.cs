using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Cinema.Persistence
{
    [Table("Employees")]
    public class Employee : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
