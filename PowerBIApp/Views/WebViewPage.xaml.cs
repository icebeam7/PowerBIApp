using PowerBIApp.ViewModels;

namespace PowerBIApp.Views;

public partial class WebViewPage : ContentPage
{
	WebViewModel vm;

	public WebViewPage(WebViewModel vm)
	{
		InitializeComponent();

		this.vm = vm;
		this.BindingContext = vm;
	}
}