using UalaSelecionado8.Models;

namespace UalaSelecionado8.Services.Authentication
{
    public interface IAuthService
    {
        public Task CheckForExpiry();
        public Task<LoginResult> Login(LoginModel loginModel);
        public Task Logout();
        public Task<RegisterResult> Register(RegisterModel registerModel);
    }
}