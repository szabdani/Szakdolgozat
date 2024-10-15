using MauiBlazorWeb.Web.Components;
using MauiBlazorWeb.Shared.Interfaces;
using MauiBlazorWeb.Web.Services;

using MauiBlazorWeb.Shared.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<IFormFactor, FormFactor>();
builder.Services.AddHttpClient();
/*
 * Blazor tanulásnál ezt másoltam ki
builder.Services
builder.Services.AddSqlite<PizzaStoreContext>("Data Source=pizza.db");
*/

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


var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
	var db = scope.ServiceProvider.GetRequiredService<TrackingAppContext>();
	if (db.Database.EnsureCreated())
	{
		SeedData.Initialize(db);
	}
}


app.Run();
