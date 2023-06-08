using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UalaSelecionado8;
using UalaSelecionado8.Services;
using UalaSelecionado8.Services.Api;
using UalaSelecionado8.Services.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore(o =>
{
    o.AddPolicy("Admin", p => p.RequireRole("Admin"));
});
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IComplyCubeService, ComplyCubeService>();
builder.Services.AddScoped(sp => new AuthenticatedHttpClient(sp.GetRequiredService<ILocalStorageService>())
{
    BaseAddress = new Uri("https://selecionadouala.wittyflower-4fe05e2c.southcentralus.azurecontainerapps.io/")
    //BaseAddress = new Uri("https://localhost:7079/")
});
builder.Services.AddMudServices();
var app = builder.Build();
var client = app.Services.GetService<AuthenticatedHttpClient>();
await client!.SetDefaultRequestHeaders();
await app.RunAsync();