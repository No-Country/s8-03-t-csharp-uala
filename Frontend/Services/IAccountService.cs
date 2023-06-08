using UalaSelecionado8.Models;

namespace UalaSelecionado8.Services
{
    public interface IAccountService
    {
        public Task<Response> GetDataAccountAsync();
    }
}
