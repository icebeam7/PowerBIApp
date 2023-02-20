using Microsoft.Identity.Client;

namespace PowerBIApp.Services
{
	public partial class Authenticator
	{
		public partial async Task<AuthenticationResult> Authenticate(string authority, string clientId, IEnumerable<string> scopes, string? returnUri = null)
		{
			AuthenticationResult authenticationResult;

			var applicationBuilder = PublicClientApplicationBuilder.Create(clientId)
				.WithRedirectUri(returnUri ?? $"msal{clientId}://auth")
				.Build();

			var accounts = await applicationBuilder.GetAccountsAsync();

			try
			{
				var firstAccount = accounts.FirstOrDefault();
				authenticationResult = await applicationBuilder.AcquireTokenSilent(scopes, firstAccount).ExecuteAsync();
			}
			catch (MsalUiRequiredException m)
			{
				var currentActivity = Platform.CurrentActivity;
				authenticationResult = await applicationBuilder.AcquireTokenInteractive(scopes).WithParentActivityOrWindow(currentActivity).ExecuteAsync();
			}

			return authenticationResult;
		}

	}
}
