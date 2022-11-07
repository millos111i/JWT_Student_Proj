using JWT.Proj.Models;
using JWT.Proj.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JWT.Proj.Services
{
    public class AuthService : IAuthService
    {
        private readonly IContext _context;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthService(IContext context, IJwtGenerator jwtGenerator)
        {
            _context = context;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<string> AuthenticateUser(LoginModel model)
        {
            var user = await _context.Users
                .Where(x => x.Login == model.Login && x.Password == model.Password)
                .FirstOrDefaultAsync();

            if (user is null)
                return string.Empty;

            return _jwtGenerator.GenerateToken();
        }

        public void CreateUser(LoginModel model)
        {
            _context.Users.Add(new Models.Core.User
            {
                Login = model.Login,
                Password = model.Password
            });

            ((Context)_context).SaveChanges();
        }
    }
}
