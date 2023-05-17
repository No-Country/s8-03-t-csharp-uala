using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using UalaSelecionado8.Services.Api;
using UalaSelecionado8.Services.Authentication;

namespace UalaSelecionado8
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddHttpClient<Client>(c =>
            {
                c.BaseAddress = new Uri("https://customapi.up.railway.app/");
                //c.BaseAddress = new Uri("https://localhost:7243/"); 
            });
            builder.Services.AddMudServices();
            await builder.Build().RunAsync();
        }
    }
}