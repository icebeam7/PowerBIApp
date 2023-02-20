using Microsoft.Identity.Client;

namespace PowerBIApp.Services
{
	public partial class Authenticator
	{
		public partial Task<AuthenticationResult> Authenticate(string authority, string clientId, IEnumerable<string> scopes, string? returnUri = null);
	}
}
