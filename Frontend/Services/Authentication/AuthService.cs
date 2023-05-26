using System.Net.Http.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using UalaSelecionado8.Models;
using UalaSelecionado8.Services.Api;

namespace UalaSelecionado8.Services.Authentication;

internal class AuthService : IAuthService
{
    private readonly AuthenticatedHttpClient _client;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;
    private readonly NavigationManager _navigationManager;

    public AuthService(AuthenticatedHttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage,
        NavigationManager navigationManager)
    {
        _client = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
        _navigationManager = navigationManager;
    }

    public async Task<RegisterResult> Register(RegisterModel registerModel)
    {
        var response = await _client.PostAsJsonAsync("api/Authenticate/Register", registerModel);
        var loginResult = JsonConvert.DeserializeObject<RegisterResult>(await response.Content.ReadAsStringAsync());
        return loginResult;
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var response = await _client.PostAsJsonAsync("api/Authenticate/Login", loginModel);
        var loginResult = JsonConvert.DeserializeObject<LoginResult>(await response.Content.ReadAsStringAsync())!;
        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }
        await _localStorage.SetItemAsync("authToken", loginResult.Token);
        await ((AuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated();
        return loginResult;
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("authToken");
        ((AuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        _client.DefaultRequestHeaders.Authorization = null;
    }

    public async Task CheckForExpiry()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;
        if (user.Identity!.IsAuthenticated)
        {
            var exp = user.Claims.Where(a => a.Type == "exp").Select(c => c.Value).SingleOrDefault();
            if (exp != null)
            {
                var time = int.Parse(exp);
                var expiredTime = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
                if (expiredTime < DateTime.UtcNow)
                {
                    await Logout();
                    _navigationManager.NavigateTo("/Login");
                }
            }
        }
    }
}