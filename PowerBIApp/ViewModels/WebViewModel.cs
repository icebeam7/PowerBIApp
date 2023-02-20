using CommunityToolkit.Mvvm.ComponentModel;

using PowerBIApp.Helpers;

namespace PowerBIApp.ViewModels
{
	[QueryProperty(nameof(WebPageUrl), Constants.ReportUrl)]
	public partial class WebViewModel : BaseViewModel
	{
		[ObservableProperty]
		string webPageUrl;
	}
}
