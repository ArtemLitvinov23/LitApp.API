using LitBlazor.Services;
using LitBlazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LitBlazor
{
    public class Program
    {
   
            public static async Task Main(string[] args)
            {
                var builder = WebAssemblyHostBuilder.CreateDefault(args);
                builder.RootComponents.Add<App>("#app");
            builder.Services
               .AddScoped<IAccountService, AccountService>()
               .AddScoped<IHttpService, HttpService>()
               .AddScoped<ILocalStorageService, LocalStorageService>();

            // configure http client
            builder.Services.AddScoped(x => {
                    var apiUrl = new Uri(builder.Configuration["apiUrl"]);

                    return new HttpClient() { BaseAddress = apiUrl };
                });

                var host = builder.Build();

                var accountService = host.Services.GetRequiredService<IAccountService>();
                await accountService.Initialize();

                await host.RunAsync();
            }
    }
}
