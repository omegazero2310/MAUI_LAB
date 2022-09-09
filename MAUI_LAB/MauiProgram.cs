using CommunityToolkit.Maui;
using DevExpress.Maui;
using MAUI_LAB.Services;
using MAUI_LAB.Services.Interface;
using MAUI_LAB.ViewModels;
using MAUI_LAB.Views;

namespace MAUI_LAB;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseDevExpress()
			.UsePrism(prism => {
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
					PlatformInitializer.OnAppStart(navigationService);
                });
            })
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
public static class PlatformInitializer
{
    public static void RegisterTypes(IContainerRegistry containerRegistry)
    {
		// Register any platform specific implementations
		containerRegistry.Register<IAdminPartServices, AdminPartServices>();
		containerRegistry.Register<IAdminStaffServices, AdminStaffServices>();
		containerRegistry.Register<IAdminUserServices, AdminUserServices>();

		containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>();
    }
	public static async void OnInit()
	{
        //
    }	
	public static async void OnAppStart(INavigationService navigationService)
	{
		//var result = await navigationService.NavigateAsync("LoginPage");
		//if (!result.Success)
		//{
		//	System.Diagnostics.Debugger.Break();
		//}
	}	
}
