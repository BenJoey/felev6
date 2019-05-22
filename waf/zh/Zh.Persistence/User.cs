using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Zh.Persistence
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
    }
}
