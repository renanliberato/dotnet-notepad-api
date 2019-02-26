using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IAuthService
    {
        Task<JwtSecurityToken> Login(string email, string password);
        Task<bool> Register(string email, string password);
    }
}