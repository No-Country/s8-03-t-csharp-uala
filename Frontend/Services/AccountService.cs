using Newtonsoft.Json;
using System.Net.Http.Json;
using UalaSelecionado8.Models;
using UalaSelecionado8.Services.Api;

namespace UalaSelecionado8.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticatedHttpClient _client;
        public AccountService(AuthenticatedHttpClient httpClient) {
        _client = httpClient;
        }

        public async Task<string> GetDataAccountAsync(string Guid) {
            var response = await _client.GetAsync($"api/Account/{Guid}");
            var accountData = JsonConvert.DeserializeObject<LoginResult>(await response.Content.ReadAsStringAsync())!;
            return "a";
        }

    }
}
