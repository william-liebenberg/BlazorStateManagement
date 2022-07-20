using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Client;
using Client.Theme;
using Client.States;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddScoped<StateManagementDemoMudTheme>();

// register our Counter state object as singleton / scoped (in Blazor Wasm this means the same thing)
builder.Services.AddSingleton<Counter>();
builder.Services.AddScoped<CounterState>();


await builder.Build().RunAsync();