using MauiBlazorWeb.Shared;
using Microsoft.Extensions.Logging;
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
			builder.Services.AddScoped<ILocalDataStorage, AndroidLocalStorage>();

            // For keeping up with the state of the app
            builder.Services.AddSingleton<IAppState, AppState>();

			// Subject-Observer for all diary components
			builder.Services.AddScoped<ISubjectComp, SubjectComp>();

			// PW hash
			builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

            // Diary DB accesses
            builder.Services.AddScoped<IDiaryManager, DiaryManager>();
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://192.168.25.230:7142/") });
			
            // Account DB accesses
			builder.Services.AddScoped<IAccountManager, AccountManager>();

			// Sport DB accesses
			builder.Services.AddScoped<ISportManager, SportManager>();

			builder.Services.AddBlazorBootstrap();

			builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
