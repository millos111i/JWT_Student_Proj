using JWT.Proj.ViewModels;

namespace JWT.Proj.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateUser(LoginModel model);
        void CreateUser(LoginModel model);
    }
}
