using ClientWithFluxor;
using ClientWithFluxor.Store;
using Shared.Clients;
using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

builder.Services.AddFluxor(o =>
{
	o.ScanAssemblies(Assembly.GetExecutingAssembly());
	o.UseReduxDevTools();
});

builder.Services.Scan(scan => scan
	.FromAssemblyOf<IFeatureClient>()
		.AddClasses(classes => classes.AssignableTo<IFeatureClient>())
			.AsSelf()
			.WithScopedLifetime());

builder.Services.Scan(scan => scan
	.FromAssemblyOf<IGeneratedClient>()
		.AddClasses(classes => classes.AssignableTo<IGeneratedClient>())
			.AsImplementedInterfaces()
			.WithScopedLifetime());

await builder.Build().RunAsync();
