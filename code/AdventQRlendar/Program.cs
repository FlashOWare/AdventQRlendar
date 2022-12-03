using AdventQRlendar;
using AdventQRlendar.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(static (IServiceProvider sp) => new QRCodeService());
builder.Services.AddSingleton(static (IServiceProvider sp) => new QRlendarService());

await builder.Build().RunAsync();
