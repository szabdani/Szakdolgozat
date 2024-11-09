using MauiBlazorWeb.Shared;
using Microsoft.Extensions.Logging;
using MauiBlazorWeb.Maui.Services;
using MauiBlazorWeb.Shared.Interfaces;
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
            // Session data
            builder.Services.AddBlazoredLocalStorage();

            // For Db services
            builder.Services.AddSingleton<IDataAccess, DataAccess>();

            // For keeping up with the state of the app
            builder.Services.AddSingleton<IAppState, AppState>();

            // PW hash
            builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Diary DB accesses
            builder.Services.AddScoped<IDiaryManager, DiaryManager>();

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
