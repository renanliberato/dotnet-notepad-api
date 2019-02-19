using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class IdentityAuthService : IAuthService
    {
        private readonly UserManager<User> _userManager;

        public IdentityAuthService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Register(string email, string password)
        {
            var user = new User {
                UserName = email,
                Email = password,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
    }
}