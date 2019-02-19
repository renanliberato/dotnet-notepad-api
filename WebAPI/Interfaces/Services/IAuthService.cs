using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace WebAPI.Services
{
    public interface IAuthService
    {
        Task<bool> Register(string email, string password);
    }
}