using LottoGenWeb;
using LottoGenWeb.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace LottoGenWeb;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        string? apiBaseAddress = builder.Configuration.GetSection("ApiSettings:BaseAddress").Value;
        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(apiBaseAddress ?? "") });
        builder.Services.AddScoped<IHttpClientWrapper, HttpClientWrapper>(); //wrapper added for unit testing

        await builder.Build().RunAsync();
    }
}