using MauiBlazorWeb.Web.Components;
using MauiBlazorWeb.Shared.Interfaces;
using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Services;
using MauiBlazorWeb.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Platform type
builder.Services.AddScoped<IPlatformService, WebPlatform>();

// Session data
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazorBootstrap();

// PW hash
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Diary DB accesses
builder.Services.AddScoped<IDiaryAPIService, DiaryAPIService>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7142") });

// Account DB accesses
builder.Services.AddScoped<IAccountAPIService, AccountAPIService>();

// Sport DB accesses
builder.Services.AddScoped<ISportAPIService, SportAPIService>();

// For keeping up with the state of the app
builder.Services.AddSingleton<IAppState, AppState>();

// Subject-Observer for all diary components
builder.Services.AddScoped<IObserverSubject, ObserverSubject>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(MauiBlazorWeb.Shared._Imports).Assembly);

app.Run();
