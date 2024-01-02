using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using Quizify.Web.App;
using Quizify.Web.BL.Extensions;
using Quizify.Web.BL.Installers;

const string defaultCultureString = "cs";
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");


var cf = builder.Configuration;
string apiBaseUrl = String.Empty;
if (builder.HostEnvironment.IsDevelopment())
{
    apiBaseUrl = cf.GetValue<string>("DEVApiBaseUrl")
    ?? throw new ArgumentException("The DEVApiBaseUrl string is missing");
}
else
{
    apiBaseUrl = cf.GetValue<string>("ApiBaseUrl")
    ?? throw new ArgumentException("The ApiBaseUrl string is missing");
}



var httpClient = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var serviceProvider = builder.Services.BuildServiceProvider();


builder.Services.AddInstaller<WebBLInstaller>(apiBaseUrl,httpClient);
builder.Services.AddScoped(_ => httpClient);

builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => httpClient);

await builder.Build().RunAsync();




