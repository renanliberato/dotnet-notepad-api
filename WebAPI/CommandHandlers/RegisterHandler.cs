using System.Threading;
using System.Threading.Tasks;
using WebAPI.Commands;
using WebAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace WebAPI.CommandHandlers
{
    public class RegisterHandler : IRequestHandler<Register, bool>
    {
        private readonly UserManager<User> _userManager;

        public RegisterHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(Register request, CancellationToken cancellationToken)
        {
            var user = new User {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            
            return result.Succeeded;
        }
    }
}