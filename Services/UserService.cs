using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Web.Http;
using TetrisAPI.Data;
using TetrisAPI.Models;
using ZstdSharp.Unsafe;

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
        /// Creates a new user and genereates a token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IdentityUser?> CreateUserAsync(User user);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(ApplicationDBContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// See interface
        /// </summary>
        public async Task<IdentityUser?> GetUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user!=null) user.Token = await GetTokenForUser(user);
            return user;
        }
        
        public async Task<IdentityUser?> CreateUserAsync(User user)
        {
            var identityUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);
            if (result.Succeeded)
            {
                var token = await GenerateUserToken(identityUser);
                await SetUserToken(identityUser, token);
                await _context.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            return await GetUserAsync(identityUser.Id);
        }

        internal async Task<string> GenerateUserToken(ApplicationUser identityUser)
        {
            var token = await _userManager.GenerateUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "AddUser");
            return token;
        }

        internal async Task SetUserToken(ApplicationUser identityUser, string token) =>
            await _userManager.SetAuthenticationTokenAsync(identityUser, TokenOptions.DefaultProvider, "AddUser", token);

        internal async Task<string?> GetTokenForUser(ApplicationUser identityUser) =>
            await _userManager.GetAuthenticationTokenAsync(identityUser, TokenOptions.DefaultProvider, "AddUser");
    }
}
