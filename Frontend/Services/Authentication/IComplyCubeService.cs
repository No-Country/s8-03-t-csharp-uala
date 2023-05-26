namespace UalaSelecionado8.Services.Authentication;

internal interface IComplyCubeService
{
    Task<ClientResult> CreateClient(object req);
    Task<SdkTokenResult> GenerateToken(string req);
}