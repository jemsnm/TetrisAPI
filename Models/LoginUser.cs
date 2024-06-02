using System.ComponentModel.DataAnnotations;

namespace TetrisAPI.Models
{
    public class LoginUser
    {
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
