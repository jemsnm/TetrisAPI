using Microsoft.AspNetCore.Identity;

namespace TetrisAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Game>? Games { get; set; }
    }
}
