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
        public IActionResult GetUser([FromRoute] int id)
        {
            var user = userService.GetUser(id);
            return user != null ? Ok(user) : Util.GenerateError($"Could not find user with id: {id}");
        }

        //[HttpPost]
        //public string Login()
        //{
        //    // will return token, token will be deleted after some random time to be decided
           
        //}
    }
}
