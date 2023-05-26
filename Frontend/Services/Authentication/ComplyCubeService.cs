using System.Net.Http.Json;
using Newtonsoft.Json;
using UalaSelecionado8.Models;


namespace UalaSelecionado8.Services.Authentication;

internal class ComplyCubeService  : IComplyCubeService
{
    private static readonly HttpClient Client = new();

    public async Task<string> GenerateToken(string req)
    {
        var response = await Client.GetAsync($"https://selecionadouala.wittyflower-4fe05e2c.southcentralus.azurecontainerapps.io/api/Authenticate/GenerateToken/{req}");
        var result = await response.Content.ReadAsStringAsync()!;
        return result;
    }
}
