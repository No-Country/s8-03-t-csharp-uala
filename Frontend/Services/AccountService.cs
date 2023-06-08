using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using UalaSelecionado8.Services.Api;

namespace UalaSelecionado8.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticatedHttpClient _client;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public AccountService(AuthenticatedHttpClient httpClient, AuthenticationStateProvider authenticationStateProvider)
        {
            _client = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response> GetDataAccountAsync()
        {
            var context = await _authenticationStateProvider.GetAuthenticationStateAsync();
            if (!context.User.Identity!.IsAuthenticated) return null;
            var guid = context.User.Claims.SingleOrDefault(a => a.Type == "sub")?.Value;
            var response = await _client.GetAsync($"api/Account/{guid}");
            var stringcontent = await response.Content.ReadAsStringAsync();
            var accountData = JsonConvert.DeserializeObject<Response>(stringcontent)!;
            return accountData;
        }

    }
    public class Account
    {
        public string Id { get; set; }
        public long AccountNumber { get; set; }
        public string UrlProfilePicture { get; set; }
        public int Balance { get; set; }
        public int InvestedBalance { get; set; }
        public long Cvu { get; set; }
        public string Alias { get; set; }
        public string OwnerId { get; set; }
    }

    public class Response
    {
        public bool Success { get; set; }
        public Account Result { get; set; }
        public string Message { get; set; }
    }
}
