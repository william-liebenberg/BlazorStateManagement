using Fluxor;
using System;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BlazorGrid;
using Microsoft.AspNetCore.Components.Web;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFluxor(o => o.ScanAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.AddSingleton<ISignalRService, DotnetSignalRService>();

builder.Services.AddLogging(o => o.SetMinimumLevel(LogLevel.Debug));

await builder.Build().RunAsync();