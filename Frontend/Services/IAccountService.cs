using UalaSelecionado8.Models;

namespace UalaSelecionado8.Services
{
    public interface IAccountService
    {
        public Task<Response> GetDataAccountAsync();
        //public Task<LoginResult> Login(LoginModel loginModel);
        //public Task Logout();
        //public Task<RegisterResult> Register(RegisterModel registerModel);
    }
}
