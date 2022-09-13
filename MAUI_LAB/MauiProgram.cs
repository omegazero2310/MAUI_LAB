using CommunityToolkit.Maui;
using MAUI_LAB.CustomControls;
using MAUI_LAB.Services;
using MAUI_LAB.Services.Interface;
using MAUI_LAB.ViewModels;
using MAUI_LAB.Views;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Mopups.Hosting;

namespace MAUI_LAB;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UsePrism(prism =>
            {
                // Register Services and setup initial Navigation
                prism.RegisterTypes(container =>
                {
                    PlatformInitializer.RegisterTypes(container);
                });
                prism.OnInitialized(() =>
                {
                    PlatformInitializer.OnInit();
                });
                prism.OnAppStart(async navigationService =>
                {
                    await PlatformInitializer.OnAppStart(navigationService);
                });
            })
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIcons");
            })
            .UseMauiCompatibility();
        builder.ConfigureMauiHandlers(collection =>
        {
#if __ANDROID__
            //collection.AddHandler(typeof(NoBorderEntry), typeof(NoBorderEntryHandlerAndroid));
#endif

        });

        return builder.Build();
    }
}
public static class PlatformInitializer
{
    public static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // Register any platform specific implementations
        containerRegistry.RegisterInstance(typeof(HttpClient));
        containerRegistry.Register<IAdminPartServices, AdminPartServices>();
        containerRegistry.Register<IAdminStaffServices, AdminStaffServices>();
        containerRegistry.Register<IAdminUserServices, AdminUserServices>();

        containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
        containerRegistry.RegisterForNavigation<MainTabbedPage, MainTabbedViewModel>();
        containerRegistry.RegisterForNavigation<HomePage, HomeViewModel>();
        containerRegistry.RegisterForNavigation<StaffListingPage, StaffListingViewModel>();
        containerRegistry.RegisterForNavigation<StaffDetailInfoPage, StaffInfoDetailViewModel>();
        containerRegistry.RegisterForNavigation<StaffEditPopupPage, StaffEditViewModel>();
        containerRegistry.RegisterForNavigation<UserNotificationPage, UserNotificationViewModel>();
        containerRegistry.RegisterForNavigation<UserAccountPage, UserAccountViewModel>();
    }
    public static async void OnInit()
    {
        //
    }
    public static async Task OnAppStart(INavigationService navigationService)
    {
        navigationService.CreateBuilder()
                    .AddSegment<LoginViewModel>()
                    .Navigate(HandleNavigationError);
    }
    private static void HandleNavigationError(Exception ex)
    {
        Console.WriteLine(ex);
        System.Diagnostics.Debugger.Break();
    }
}
