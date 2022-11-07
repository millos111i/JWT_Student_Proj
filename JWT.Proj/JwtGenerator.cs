using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace JWT.Proj
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly Auth _authOptions;

        public JwtGenerator(IOptions<Auth> options)
        {
            _authOptions = options.Value;
        }

        public string GenerateToken()
        {
            var key = Encoding.ASCII.GetBytes(_authOptions.Token);
            var handler = new JwtSecurityTokenHandler();
            var token = handler
                .CreateJwtSecurityToken(new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddMinutes(_authOptions.ExpirationTime),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                });

            return handler.WriteToken(token);
        }
    }
}
