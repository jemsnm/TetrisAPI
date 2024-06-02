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
        /// Creates a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<IdentityUser?> CreateUserAsync(SignUpUser user);

        /// <summary>
        /// Login a user and return a token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        Task<string?> LoginUserAsync(LoginUser userInfo);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(ApplicationDBContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityUser?> GetUserAsync(string id) => 
            await _userManager.FindByIdAsync(id);
        
        public async Task<IdentityUser?> CreateUserAsync(SignUpUser user)
        {
            var identityUser = new ApplicationUser
            {
                UserName = user.UserName,
                Email = user.Email
            };

            var result = await _userManager.CreateAsync(identityUser, user.Password);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
            }
            else
            {
                return null;
            }

            return await GetUserAsync(identityUser.Id);
        }

        public async Task<string?> LoginUserAsync(LoginUser userInfo)
        {
            var user = await GetUserByEmailAsync(userInfo.Email);
            if (user!= null) {
                var result = await _signInManager.PasswordSignInAsync(user, userInfo.Password, false, false);
                if (result.Succeeded)
                {
                    var token = await GenerateUserTokenAsync(user);
                    await SetUserTokenAsync(user, token);
                    await _context.SaveChangesAsync();
                    return token;
                }
            }

            return null;
        }

        internal async Task<ApplicationUser?> GetUserByEmailAsync(string email) =>
            await _userManager.FindByEmailAsync(email);

        internal async Task<string> GenerateUserTokenAsync(ApplicationUser identityUser) =>
            await _userManager.GenerateUserTokenAsync(identityUser, TokenOptions.DefaultProvider, "UserLogin");

        internal async Task SetUserTokenAsync(ApplicationUser identityUser, string token) =>
            await _userManager.SetAuthenticationTokenAsync(identityUser, TokenOptions.DefaultProvider, "UserLogin", token);

        internal async Task<string?> GetTokenForUserAsync(ApplicationUser identityUser) =>
            await _userManager.GetAuthenticationTokenAsync(identityUser, TokenOptions.DefaultProvider, "UserLogin");
    }
}
