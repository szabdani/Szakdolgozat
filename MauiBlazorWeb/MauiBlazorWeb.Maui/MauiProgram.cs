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
			builder.Services.AddBlazoredLocalStorage();
			builder.Services.AddScoped<ILocalDataStorage, AndroidLocalStorage>();

            // For keeping up with the state of the app
            builder.Services.AddSingleton<IAppState, AppState>();

			// Subject-Observer for all diary components
			builder.Services.AddScoped<ISubjectComp, SubjectComp>();

			// PW hash
			builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

			// Diary DB accesses

			var handler = new HttpClientHandler{ ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true };
			var client = new HttpClient(handler){ BaseAddress = new Uri("https://192.168.25.230:7142/") };
			builder.Services.AddSingleton(client);


			builder.Services.AddScoped<IDiaryAPIService, DiaryAPIService>();
			

			// Account DB accesses
			builder.Services.AddScoped<IAccountAPIService, AccountAPIService>();

			// Sport DB accesses
			builder.Services.AddScoped<ISportAPIService, SportAPIService>();

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
