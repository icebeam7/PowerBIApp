using PowerBIApp.Views;
using PowerBIApp.Helpers;

namespace PowerBIApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(Constants.DetailsRoute, typeof(WebViewPage));
	}
}
