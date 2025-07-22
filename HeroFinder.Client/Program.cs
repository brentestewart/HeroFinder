using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HeroFinder.Client.Services;
using System.Net.Http;
using AutoMapper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register HttpClient for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<HeroMappingProfile>());

// Register HeroApiService for DI
builder.Services.AddScoped<HeroApiService>();

await builder.Build().RunAsync();
