using Microsoft.Extensions.Logging;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;
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

            builder.Services.AddTransient<ScheldudeViewModel>();
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddTransient<WorkerPickerPage>();
            builder.Services.AddTransient<WorkerPickerViewModel>();
            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<ScheldudePage>();
            builder.Services.AddSingleton<FakeDataFactory>();
            builder.Services.AddSingleton<IDataStorage>(new DataStorageTemp());
            builder.Services.AddSingleton<IAPIService>(provider => new APITemp(provider.GetRequiredService<IDataStorage>(),provider.GetRequiredService<FakeDataFactory>()));


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}