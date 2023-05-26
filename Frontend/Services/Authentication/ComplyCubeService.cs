using System.Net.Http.Headers;
using Microsoft.JSInterop;
using Newtonsoft.Json;


namespace UalaSelecionado8.Services.Authentication;

internal class ComplyCubeService  : IComplyCubeService
{
    private readonly IJSRuntime _jsRuntime;
    public ComplyCubeService(IJSRuntime js)
    {
        _jsRuntime = js;
    }
    public async Task<ClientResult> CreateClient(object req)
    {
        var stringobj = JsonConvert.SerializeObject(req);
        var response = await _jsRuntime.InvokeAsync<object>("CreateUser", stringobj);
        var Result = JsonConvert.DeserializeObject<ClientResult>((string)response)!;
        return Result;
    }
    public async Task<SdkTokenResult> GenerateToken(string req)
    {
        var response = await _jsRuntime.InvokeAsync<object>("CreateUser", req);
        var Result = JsonConvert.DeserializeObject<SdkTokenResult>((string)response)!;
        return Result;
    }

}
public class ClientResult
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string Telephone { get; set; }
    public string JoinedDate { get; set; }
    public Persondetails PersonDetails { get; set; }
    public Metadata Metadata { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
public class Persondetails
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Dob { get; set; }
    public string Nationality { get; set; }
}
public class Metadata
{
    public string AccountReference { get; set; }
}
public class SdkTokenResult
{
    public string Token { get; set; }
}