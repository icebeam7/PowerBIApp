using PowerBIApp.ViewModels;

namespace PowerBIApp.Views;

public partial class ReportsListPage : ContentPage
{
	ReportsListViewModel vm;

	public ReportsListPage(ReportsListViewModel vm)
	{
		InitializeComponent();

		this.vm = vm;
		this.BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (vm != null)
		{
			this.vm.SelectedReport = null;
			this.vm.IsReportsListRefreshing = true;
		}
	}
}