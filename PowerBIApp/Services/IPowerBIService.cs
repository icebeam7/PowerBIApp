using Microsoft.PowerBI.Api.Models;

namespace PowerBIApp.Services
{
	public interface IPowerBIService
	{
		Task<Reports> GetReports();
	}
}
