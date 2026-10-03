using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechNotes.Application.Notes;
using TechNotes.Client.Features.Notes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<INoteOverviewService, NotesOverviewServiceClient>();

await builder.Build().RunAsync();
