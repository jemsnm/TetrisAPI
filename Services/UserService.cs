using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Web.Http;
using TetrisAPI.Data;
using TetrisAPI.Models;

namespace TetrisAPI.Services
{
    public interface IUserService
    {
        User? GetUser(int id);
    }

    public class UserService : IUserService
    {
        private readonly DBContext context;

        public UserService(DBContext context) 
        {
            this.context = context;
        }

        public User? GetUser(int id)
        {
            return context.Users.Find(id);
        }
    }
}
