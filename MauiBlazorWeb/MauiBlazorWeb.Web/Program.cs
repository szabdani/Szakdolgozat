using MauiBlazorWeb.Web.Components;
using MauiBlazorWeb.Shared.Interfaces;
using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Services;
using MauiBlazorWeb.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// For Db services
builder.Services.AddSingleton<IDataAccess, DataAccess>();

// Session data
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<ILocalDataStorage, PCLocalStorage>();

builder.Services.AddBlazorBootstrap();

// PW hash
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

// Diary DB accesses
builder.Services.AddScoped<IDiaryManager, DiaryManager>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7142") });
// Sport DB accesses
builder.Services.AddScoped<ISportManager, SportManager>();

// For keeping up with the state of the app
builder.Services.AddSingleton<IAppState, AppState>();

// Subject-Observer for all diary components
builder.Services.AddScoped<ISubjectComp, SubjectComp>();

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
