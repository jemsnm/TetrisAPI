using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using TetrisAPI.Libraries;
using TetrisAPI.Models;
using TetrisAPI.Services;

namespace TetrisAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] string id)
        {
            var user = await userService.GetUserAsync(id);
            return user != null ? Ok(user) : Util.GenerateError($"Could not find user with id: {id}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var createdUser = await userService.CreateUserAsync(user);
            return createdUser != null ? Ok(createdUser) : Util.GenerateError($"User cannot be created");
        }
    }
}
