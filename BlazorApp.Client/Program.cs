using BlazorApp.Client.Services;
using Duende.Bff.Blazor.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(http => new HttpClient
{
	BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

/* Identity Server https://github.com/DuendeSoftware/samples/blob/main/BFF/v3/BlazorWasm/Client/Program.cs */
builder.Services
	.AddBffBlazorClient()
	.AddCascadingAuthenticationState();

builder.Services.AddLocalApiHttpClient("backend");
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));
/***********************************/

builder.Services.AddSingleton<IRepository, CustomersService>();
//builder.Services.AddSingleton<IJSRuntime, JSRuntime>();

await builder.Build().RunAsync();
