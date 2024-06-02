using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TetrisAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Game>? Games { get; set; }

        [NotMapped]
        public string? Token { get; set; }
    }
}
