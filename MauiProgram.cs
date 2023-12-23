using Microsoft.Extensions.Logging;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;
using TheJobOrganizationApp.Services;
using Mopups.Hosting;

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
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("SF-Pro.ttf", "MainFont");
                    fonts.AddFont("SF-Pro-Display-Medium.otf", "BoldFont");
                });
            builder.Services.AddSingleton(MopupService.Instance);
            builder.Services.AddSingleton<IDataStorage>(provider => new DataStorageTemp(provider.GetRequiredService<GlobalSettings>()));
            builder.Services.AddSingleton<ILoadableContent>(new LoadableContent());
            builder.Services.AddSingleton<FakeDataFactory>();
            builder.Services.AddSingleton<Initializator>();
            builder.Services.AddSingleton<GlobalSettings>();
            builder.Services.AddSingleton<GSSelector>();
            builder.Services.AddSingleton<PageFactory>();
            builder.Services.AddSingleton<IAPIService>(provider => new APITemp(provider.GetRequiredService<IDataStorage>(), provider.GetRequiredService<FakeDataFactory>(), 10));
            builder.Services.AddTransient<ScheldudeViewModel>();
            builder.Services.AddSingleton<Resources>();
            builder.Services.AddSingleton<GlobalSearchViewModel>();
            builder.Services.AddSingleton<WorkerDetailsViewModel>();
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddTransient<WorkerPickerViewModel>();
            builder.Services.AddSingleton<WorkerPickerPage>();
            builder.Services.AddSingleton<ModelToVMAdaptor>();
            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<ScheldudePage>();
            builder.Services.AddSingleton<GlobalSearchPage>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}