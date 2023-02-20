using PowerBIApp.Views;
using PowerBIApp.Services;
using PowerBIApp.ViewModels;

namespace PowerBIApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IPowerBIService, PowerBIService>();
		//builder.Services.AddSingleton<IAuthenticator, Authenticator>();

		builder.Services.AddTransient<ReportsListViewModel>();
		builder.Services.AddTransient<ReportsListPage>();

		builder.Services.AddTransient<WebViewModel>();
		builder.Services.AddTransient<WebViewPage>();

		return builder.Build();
	}
}
