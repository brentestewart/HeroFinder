using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using HeroFinder.Client.Services;
using HeroFinder.ComponentLibrary.ViewModels;
using System.Net.Http;
using AutoMapper;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Register HttpClient for API calls
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// Register AutoMapper
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<HeroMappingProfile>());

// Register HeroApiService for DI
builder.Services.AddScoped<HeroApiService>();

// Register all ViewModels automatically - explicitly pass the current assembly to ensure ViewModels in this project are found
builder.Services.AddViewModels(Assembly.GetExecutingAssembly());

await builder.Build().RunAsync();
