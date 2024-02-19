using Microsoft.Extensions.Logging;
using TheJobOrganizationApp.View;
using TheJobOrganizationApp.ViewModels;
using Syncfusion.Maui.Core.Hosting;
using TheJobOrganizationApp.Services;
using Mopups.Hosting;
using Mopups.Services;
using TheJobOrganizationApp.Services.Interfaces;
using TheJobOrganizationApp.Services.HighLevelServices;
using TheJobOrganizationApp.ViewModels.PopUpViewModels;
using TheJobOrganizationApp.ViewModels.MainViewModels;
using TheJobOrganizationApp.Services.LowLeveLServices;
using TheJobOrganizationApp.View.DetailsPages;

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
            #region Services
            builder.Services.AddSingleton(MopupService.Instance);
            builder.Services.AddTransient<IErrorService, ErrorService>();
            builder.Services.AddSingleton<IDataStorage,DataStorageTemp>();
            builder.Services.AddSingleton<IConnectionService, APITemp>();
            builder.Services.AddSingleton<IUserController, UserController>();
            builder.Services.AddSingleton<ISettings,AppSettings>();
            builder.Services.AddTransient<IInitializator,Initializator>();
            builder.Services.AddTransient<FakeDataFactory>();
            builder.Services.AddTransient<PageFactory>();
            #endregion
            #region XAML and Reflection Content and Converter between them
            builder.Services.AddSingleton<IReflectionContent, RuntimeContent>();
            builder.Services.AddSingleton<IXAMLContent, RuntimeContent>
                (sp => sp.GetRequiredService<IReflectionContent>() as RuntimeContent);            
            builder.Services.AddSingleton<IConverter,RuntimeContent>
                (sp => sp.GetRequiredService<IReflectionContent>() as RuntimeContent);
            #endregion
            #region User Controller
            builder.Services.AddSingleton<IUserController, UserController>();
            #endregion
            #region ViewModels
            builder.Services.AddTransient<ScheldudeViewModel>();
            builder.Services.AddTransient<GlobalSearchViewModel>();
            builder.Services.AddTransient<LogInViewModel>();
            builder.Services.AddTransient<WorkerPickerViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            #endregion
            #region Pages
            builder.Services.AddTransient<WorkerPickerPage>();
            builder.Services.AddTransient<LogInPage>();
            builder.Services.AddTransient<ScheldudePage>();
            builder.Services.AddTransient<GlobalSearchPage>();
            builder.Services.AddTransient<AssignmentDetailsPage>();
            builder.Services.AddTransient<SettingsPage>();
            #endregion
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
    handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });
            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Editor), (handler, view) =>
            {
#if ANDROID
    handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });
            Microsoft.Maui.Handlers.PickerHandler.Mapper.AppendToMapping(nameof(Picker), (handler, view) =>
            {
#if ANDROID
    handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}