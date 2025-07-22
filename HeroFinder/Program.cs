using AutoMapper;
using HeroFinder.Client.Services; // For HeroMappingProfile
using HeroFinder.Components;
using HeroFinder.ComponentLibrary;
using HeroFinder.Services;
using HeroFinder.Repositories;
using HeroFinder.Shared.Repositories;
using HeroFinder.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers(); // Add this line

// Register interfaces and implementations for DI
builder.Services.AddSingleton<IHeroRepository, HeroRepository>();
builder.Services.AddSingleton<IHeroService, HeroService>();
builder.Services.AddScoped<HeroApiService>();

// Register HttpClient for server-side Blazor (required for InteractiveAuto)
var baseAddress = builder.Configuration["BaseAddress"] ?? "https://localhost:7043";
builder.Services.AddHttpClient("Default", client =>
{
    client.BaseAddress = new Uri(baseAddress);
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<HeroMappingProfile>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllers(); // Add this line
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(HeroFinder.Client._Imports).Assembly);

app.Run();
