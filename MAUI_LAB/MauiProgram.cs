using CommunityToolkit.Maui;

namespace MAUI_LAB;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
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
    }
	public static async void OnInit()
	{
        //
    }	
	public static async void OnAppStart(INavigationService navigationService)
	{
        await navigationService.NavigateAsync("NavigationPage/MainPage");
    }	
}
