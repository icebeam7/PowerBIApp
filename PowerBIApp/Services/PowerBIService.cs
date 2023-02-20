using Microsoft.Rest;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;

using PowerBIApp.Helpers;

namespace PowerBIApp.Services
{
	public class PowerBIService : IPowerBIService
	{
		//IAuthenticator authenticator { get; set; }

		public PowerBIService()
			//IAuthenticator authenticator)
		{
			//this.authenticator = authenticator;
		}

		public async Task<Reports> GetReports()
		{
			await UpdateActivityIndicatorStatus(true);

			try
			{
				var client = await GetPowerBIClient();
				return client.Reports.GetReports();
			}
			catch (Exception e)
			{
				return new Reports();
			}
			finally
			{
				await UpdateActivityIndicatorStatus(false);
			}
		}

		int _networkIndicatorCount;
		PowerBIClient? _powerBIClient;

		string AccessToken
		{
			get => Preferences.Get(nameof(AccessToken), string.Empty);
			set => Preferences.Set(nameof(AccessToken), value);
		}

		DateTimeOffset AccessTokenExpiresOnDateTimeOffset
		{
			get
			{
				DateTimeOffset expirationAsDateTimeOffset;

				var expirationAsString = Preferences.Get(nameof(AccessTokenExpiresOnDateTimeOffset), string.Empty);

				if (string.IsNullOrWhiteSpace(expirationAsString))
					expirationAsDateTimeOffset = new DateTimeOffset(0, 0, 0, 0, 0, 0, TimeSpan.FromMilliseconds(0));
				else
					DateTimeOffset.TryParse(expirationAsString, out expirationAsDateTimeOffset);

				return expirationAsDateTimeOffset;
			}
			set
			{
				var dateTimeOffsetAsString = value.ToString();
				Preferences.Set(nameof(AccessTokenExpiresOnDateTimeOffset), dateTimeOffsetAsString);
			}
		}

		async ValueTask<PowerBIClient> GetPowerBIClient()
		{
			if (_powerBIClient is null)
			{
				await Authenticate();
				_powerBIClient = new PowerBIClient(new TokenCredentials(AccessToken));
			}

			return _powerBIClient;
		}

		async Task UpdateActivityIndicatorStatus(bool isActivityIndicatorDisplayed)
		{
			if (isActivityIndicatorDisplayed)
			{
				_networkIndicatorCount++;
				await updateIsBusy(true);
			}
			else if (--_networkIndicatorCount <= 0)
			{
				_networkIndicatorCount = 0;
				await updateIsBusy(false);
			}
		}

		Task updateIsBusy(bool isBusy) =>
			MainThread.InvokeOnMainThreadAsync(
				() => Application.Current.MainPage.IsBusy = isBusy);

		async Task Authenticate()
		{
			if (string.IsNullOrWhiteSpace(AccessToken)
					|| DateTimeOffset.UtcNow.CompareTo(AccessTokenExpiresOnDateTimeOffset) >= 1)
			{
				await UpdateActivityIndicatorStatus(true);

				try
				{
					var authenticator = new Authenticator();

					var authenticationResult = await authenticator.Authenticate(
						Constants.OAuth2Authority,
						Constants.ApplicationId,
						Constants.Scopes);

					AccessToken = authenticationResult.AccessToken;
					AccessTokenExpiresOnDateTimeOffset = authenticationResult.ExpiresOn;
				}
				finally
				{
					await UpdateActivityIndicatorStatus(false);
				}
			}
		}
	}
}
