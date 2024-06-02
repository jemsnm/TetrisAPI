using System.ComponentModel.DataAnnotations;

namespace TetrisAPI.Models
{
    public class SignUpUser : LoginUser
    {
        public required string UserName { get; set; }
    }
}
