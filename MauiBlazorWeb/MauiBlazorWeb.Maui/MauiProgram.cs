using MauiBlazorWeb.Shared;
using Microsoft.Extensions.Logging;
using MauiBlazorWeb.Maui.Services;
using MauiBlazorWeb.Shared.Interfaces;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MauiBlazorWeb.Shared.Data;
using MauiBlazorWeb.Shared.Models;

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

            // Identity configuration
            var connectionString = "server=localhost;port=3306;database=diary_db;user=root;password=";
            builder.Services.AddDbContext<TrackingAppContext>(options =>
		        options.UseMySQL(connectionString));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<TrackingAppContext>()
				.AddDefaultTokenProviders();


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
