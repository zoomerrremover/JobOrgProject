using Microsoft.Extensions.Logging;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;
using TheJobOrganizationApp.Services;
using Mopups.Hosting;
using CommunityToolkit.Maui.Maps;
using Mopups.Services;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Services.HighLevelServices;
using TheJobOrganizationApp.ViewModels.PopUpViewModels;
using TheJobOrganizationApp.ViewModels.MainViewModels;
using TheJobOrganizationApp.Services.LowLeveLServices;
using TheJobOrganizationApp.ViewModels.Base;

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
            builder.Services.AddTransient<IErrorService, ErrorService>();
            builder.Services.AddSingleton(MopupService.Instance);
            builder.Services.AddSingleton<IDataStorage,DataStorageTemp>();
            builder.Services.AddSingleton<ILoadableContent>(new ContentLoader());
            builder.Services.AddSingleton<IAPIService, APITemp>();
            #region XAML and Reflection Content and Converter between them
            builder.Services.AddSingleton<IReflectionContent, RuntimeContent>();
            builder.Services.AddSingleton<IXAMLContent, RuntimeContent>
                (sp => sp.GetRequiredService<IReflectionContent>() as RuntimeContent);            
            builder.Services.AddSingleton<IConverter,RuntimeContent>
                (sp => sp.GetRequiredService<IReflectionContent>() as RuntimeContent);
            #endregion
            #region User Controller
            builder.Services.AddSingleton<IUserController, UserController>();
            builder.Services.AddSingleton<IUserPermissionController, UserController>
                (sp => sp.GetRequiredService<IUserController>() as UserController);
            #endregion
            builder.Services.AddSingleton<ModelView>();
            builder.Services.AddSingleton<FakeDataFactory>();
            builder.Services.AddSingleton<GlobalSettings>();
            builder.Services.AddSingleton<PageFactory>();
            builder.Services.AddTransient<ScheldudeViewModel>();
            builder.Services.AddSingleton<GlobalSearchViewModel>();
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddTransient<WorkerPickerViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddSingleton<WorkerPickerPage>();
            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<ScheldudePage>();
            builder.Services.AddSingleton<GlobalSearchPage>();
            builder.Services.AddTransient<SettingsPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}