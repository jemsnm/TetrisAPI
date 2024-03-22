﻿using System.ComponentModel.DataAnnotations;

namespace TetrisAPI.Models
{
    public class User
    {
        public required string UserName { get; set; }

        public required string Password { get; set; }

        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public required string Email { get; set; }
    }
}
