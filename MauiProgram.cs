using Microsoft.Extensions.Logging;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;
using Bogus;
using TheJobOrganizationApp.Services;

namespace TheJobOrganizationApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureSyncfusionCore();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ScheldudeViewModel>();
            builder.Services.AddSingleton<LogInViewModel>();
            builder.Services.AddSingleton<LogInPage>();
            builder.Services.AddSingleton<ScheldudePage>();
            builder.Services.AddSingleton<FakeDataFactory>();
            builder.Services.AddSingleton<IDataStorage>();
            builder.Services.AddSingleton<IAPIService>(new APITemp());

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}