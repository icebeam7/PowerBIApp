namespace PowerBIApp.Helpers
{
	public class Constants
	{
		public const string ApplicationId = "";

		//public const string RedirectUrl = "https://login.microsoftonline.com/common/oauth2/nativeclient";

		public const string OAuth2Authority = "https://login.microsoftonline.com/common";

		public static IReadOnlyList<string> Scopes { get; } = new[]
		{
			"User.Read",
			"https://analysis.windows.net/powerbi/api/Report.Read.All"
		};

		public const string DetailsRoute = "DetailsPage";
		public const string ReportUrl = "ReportUrl";
	}
}
