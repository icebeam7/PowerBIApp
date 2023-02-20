using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.PowerBI.Api.Models;

using PowerBIApp.Services;
using PowerBIApp.Helpers;

namespace PowerBIApp.ViewModels
{
	public partial class ReportsListViewModel : BaseViewModel
	{
		[ObservableProperty]
		bool isReportsListRefreshing;

		[ObservableProperty]
		Report selectedReport;

		public ObservableCollection<Report> VisibleReportsListData { get; } = new();

		IPowerBIService service;

		public ReportsListViewModel(IPowerBIService service)
		{
			this.service = service;
		}

		[RelayCommand]
		async Task RefreshReportsList()
		{
			VisibleReportsListData.Clear();

			try
			{
				var reports = await service.GetReports();

				foreach (var report in reports.Value.OrderBy(x => x.Name))
					VisibleReportsListData.Add(report);
			}
			finally
			{
				IsReportsListRefreshing = false;
			}
		}

		[RelayCommand]
		async Task ViewReport()
		{
			if (SelectedReport != null)
			{
				var data = new Dictionary<string, object>()
				{
					{
						Constants.ReportUrl, SelectedReport.WebUrl
					}
				};

				await Shell.Current.GoToAsync(Constants.DetailsRoute, true, data);
			}
		}

	}
}