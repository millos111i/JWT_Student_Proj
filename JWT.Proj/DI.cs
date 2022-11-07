using JWT.Proj.Models;
using JWT.Proj.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace JWT.Proj
{
    public static class DI
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<Auth>()
                .Bind(configuration.GetSection(nameof(Auth)));

            services.AddDbContext<Context>(options => options.UseInMemoryDatabase(databaseName: "Jwt_Test"));

            services.AddScoped<IContext, Context>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.ASCII.GetBytes(configuration.GetSection("Auth:Token").Value)),
                    ValidateLifetime = true,
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

            services.AddAuthorization();
        }
    }
}
