using MediatR;

namespace WebAPI.Commands 
{
    public class Login : IRequest<bool>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Login(
            string Email,
            string Password
        )
        {
            this.Email = Email;
            this.Password = Password;
        }
    }
}