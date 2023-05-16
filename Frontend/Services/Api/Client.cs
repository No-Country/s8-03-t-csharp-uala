using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace UalaSelecionado8.Services.Api
{
    public class Client
    {
        public readonly HttpClient httpClient;
        private readonly ILocalStorageService _localStorage;
        public static string token;
        public Client(HttpClient httpClient, ILocalStorageService localStorage)
        {
            this.httpClient = httpClient;
            _localStorage = localStorage;
            Task.Run(() => { SetAuthToken().Wait(); });
        }
        private async Task SetAuthToken()
        {
            if (token is null)
            {
                token = await _localStorage.GetItemAsync<string>("authToken");
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
    }
}
