namespace UalaSelecionado8.Services.Authentication;

internal interface IComplyCubeService
{
    Task<string?> GenerateToken(string req);
}