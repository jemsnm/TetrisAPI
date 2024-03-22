using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using TetrisAPI.Data;
using TetrisAPI.Models;

namespace TetrisAPI.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Gets a user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IdentityUser?> GetUserAsync(string id);

        /// <summary>
        /// Saves a user, generates a token and returns the token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> SaveUserAsync(User user);
    }

    public class UserService : IUserService
    {
        private readonly DBContext context;
        private readonly UserManager<IdentityUser>  userManager;

        public UserService(DBContext context, UserManager<IdentityUser> userManager) 
        {
            this.context = context;
            this.userManager = userManager;
        }

        /// <summary>
        /// See interface
        /// </summary>
        public async Task<IdentityUser?> GetUserAsync(string id) =>
            await userManager.FindByIdAsync(id);

        /// <summary>
        /// See interface
        /// </summary>
        public async Task<string> SaveUserAsync(User user) 
        {
            var identityUser = new IdentityUser()
            {
                UserName = user.UserName,
                Email = user.Email
            };

            context.Users.Add(identityUser);
;           await userManager.CreateAsync(identityUser, user.Password);

            return await GenerateUserTokenAsync(identityUser);
        }

        /// <summary>
        /// Generates and returns a user token
        /// </summary>
        /// <param name="identityUser"></param>
        /// <returns></returns>
        private async Task<string> GenerateUserTokenAsync(IdentityUser identityUser) =>
            await userManager.GenerateUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "AddUser");
    }
}
