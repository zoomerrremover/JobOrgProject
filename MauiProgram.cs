using Microsoft.Extensions.Logging;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;
using TheJobOrganizationApp.Services;
using Mopups.Hosting;
using Mopups.Interfaces;
using Mopups.Services;

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
                .ConfigureMopups()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<IPopupNavigation>(MopupService.Instance);
            builder.Services.AddTransient<ScheldudeViewModel>();
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddSingleton<WorkerPickerPage>();
            builder.Services.AddTransient<WorkerPickerViewModel>();
            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<ScheldudePage>();
            builder.Services.AddSingleton<FakeDataFactory>();
            builder.Services.AddSingleton<GlobalSearchPage>();
            builder.Services.AddSingleton<GlobalSearchViewModel>();
            builder.Services.AddSingleton<IDataStorage>(new DataStorageTemp());
            builder.Services.AddSingleton<ILoadableContent>(new LoadableContent());
            builder.Services.AddSingleton<Initializator>();
            builder.Services.AddSingleton<IAPIService>(provider => new APITemp(provider.GetRequiredService<IDataStorage>(),provider.GetRequiredService<FakeDataFactory>(),10));
            builder.Services.AddSingleton<SharedControls>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}