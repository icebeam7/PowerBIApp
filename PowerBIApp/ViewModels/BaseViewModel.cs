using CommunityToolkit.Mvvm.ComponentModel;

namespace PowerBIApp.ViewModels
{
	public partial class BaseViewModel : ObservableObject
	{
		[ObservableProperty]
		bool isBusy;
	}
}
