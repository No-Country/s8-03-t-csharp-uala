using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using UalaSelecionado8.Services.Api;

namespace UalaSelecionado8;

internal class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly AuthenticatedHttpClient _client;
    public AuthStateProvider(ILocalStorageService localStorage, AuthenticatedHttpClient client)
    {
        _localStorage = localStorage;
        _client = client;
    }
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var savedToken = await _localStorage.GetItemAsync<string>("authToken");
        if (string.IsNullOrWhiteSpace(savedToken))
        {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        await _localStorage.SetItemAsync("authToken", savedToken);
        var identity = ParseClaimsFromJwt(savedToken);
        var exp = identity.Claims.Where(a => a.Type == "exp").Select(c => c.Value).SingleOrDefault();
        if (exp != null)
        {
            var time = int.Parse(exp);
            var expiredTime = DateTimeOffset.FromUnixTimeSeconds(time).DateTime;
            if (expiredTime < DateTime.UtcNow)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
        }
        await _client.SetDefaultRequestHeaders();
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task MarkUserAsAuthenticated()
    {
        await _client.SetDefaultRequestHeaders();
        var savedToken = await _localStorage.GetItemAsync<string>("authToken");
        var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(ParseClaimsFromJwt(savedToken))));
        NotifyAuthenticationStateChanged(authState);
    }

    public void MarkUserAsLoggedOut()
    {
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }
    private static ClaimsIdentity ParseClaimsFromJwt(string jwtToken)
    {
        var claims = new List<Claim>();
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var claimsPrincipal = tokenHandler.ReadJwtToken(jwtToken);
            var identity = new ClaimsIdentity(claimsPrincipal.Claims, "jwt");
            return identity;
        }
        catch (Exception)
        {
            return new ClaimsIdentity();
        }
    }

}