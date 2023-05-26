using System.Net.Http.Json;
using Newtonsoft.Json;
using UalaSelecionado8.Models;


namespace UalaSelecionado8.Services.Authentication;

internal class ComplyCubeService  : IComplyCubeService
{
    private static readonly HttpClient Client = new();

    public async Task<string> GenerateToken(string req)
    {
        var response = await Client.GetAsync($"api/Authenticate/GenerateToken/{req}");
        var result = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync())!;
        return result;
    }
}
