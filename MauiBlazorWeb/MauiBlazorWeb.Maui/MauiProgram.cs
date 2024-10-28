using MauiBlazorWeb.Shared;
using Microsoft.Extensions.Logging;
using MauiBlazorWeb.Maui.Services;
using MauiBlazorWeb.Shared.Interfaces;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using MauiBlazorWeb.Shared.Services;

namespace MauiBlazorWeb.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            InteractiveRenderSettings.ConfigureBlazorHybridRenderModes();

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // For Db services
            builder.Services.AddSingleton<IDataAccess, DataAccess>();

            // Session data
            builder.Services.AddBlazoredLocalStorage();

            // PW hash
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            return builder.Build();
        }
    }
}
