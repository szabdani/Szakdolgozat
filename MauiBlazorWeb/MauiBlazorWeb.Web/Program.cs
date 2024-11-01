using MauiBlazorWeb.Web.Components;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Web.Services;
using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// For Db services
builder.Services.AddSingleton<IDataAccess, DataAccess>();

// Session data
builder.Services.AddBlazoredLocalStorage();

// PW hash
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// For keeping up with the state of the app
builder.Services.AddSingleton<IAppState, AppState>();

builder.Services.AddScoped<IFormFactor, FormFactor>();

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
