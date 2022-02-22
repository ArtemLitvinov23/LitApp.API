using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using MudBlazor;
using MudBlazor.Services;
using LitChat.Blazor.Services.Interfaces;
using LitChat.Blazor.Services;

namespace LitChat.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services
               .AddScoped<IAccountService, AccountService>()
               .AddScoped<IFavoritesListService, FavoritesListService>()
               .AddScoped<IProfileService, UserProfileService>()
               .AddScoped<IHttpServiceGeneric, HttpServiceGeneric>()
               .AddScoped<ILocalStorageService, LocalStorageService>()
               .AddScoped<IConnectionService, ConnectionService>()
               .AddScoped<IFriendService, FriendService>()
               .AddTransient<IChatService, ChatService>();

            // configure http client
            builder.Services.AddScoped(x =>
            {
                var apiUrl = new Uri(builder.Configuration["apiUrl"]);

                return new HttpClient() { BaseAddress = apiUrl };
            });

            builder.Services.AddMudServices(c => { c.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight; });
            var host = builder.Build();
            builder.Services.AddApiAuthorization();
            var accountService = host.Services.GetRequiredService<IAccountService>();
            await accountService.Initialize();

            await host.RunAsync();
        }
    }
}
