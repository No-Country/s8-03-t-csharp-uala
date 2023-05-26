using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace UalaSelecionado8.Services.Api;

public class AuthenticatedHttpClient : HttpClient
{
    private readonly ILocalStorageService _localStorage;
    public AuthenticatedHttpClient(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }
    public async Task SetDefaultRequestHeaders()
    {
        if (DefaultRequestHeaders.Authorization != null) return;
        var accessToken = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(accessToken))
        {
            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }
    }
}
