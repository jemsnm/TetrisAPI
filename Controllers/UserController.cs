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
        public IActionResult GetUser([FromRoute] string id)
        {
            var user = userService.GetUserAsync(id);
            return user != null ? Ok(user) : Util.GenerateError($"Could not find user with id: {id}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            var userToken = await userService.SaveUserAsync(user);
            return userToken != null ? Ok(userToken) : Util.GenerateError($"Cannot return token for user:{user.UserName}");
        }
    }
}
