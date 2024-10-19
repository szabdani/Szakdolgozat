using MauiBlazorWeb.Shared;
using Microsoft.Extensions.Logging;
using MauiBlazorWeb.Maui.Services;
using MauiBlazorWeb.Shared.Interfaces;
using DataLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

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
