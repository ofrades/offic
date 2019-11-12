using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Shared;

namespace Client {

	/// <summary>
	/// ServerAuthenticationStateProvider
	/// </summary>
	public class ServerAuthenticationStateProvider : AuthenticationStateProvider {
		private readonly HttpClient _httpClient;

		/// <summary>
		/// ServerAuthenticationStateProvider Constructor
		/// </summary>
		/// <param name="httpClient"></param>
		public ServerAuthenticationStateProvider (HttpClient httpClient) {
			_httpClient = httpClient;
		}

		/// <summary>
		/// GetAuthenticationStateAsync
		/// </summary>
		/// <returns></returns>
		public override async Task<AuthenticationState> GetAuthenticationStateAsync () {
			var userInfo = await _httpClient.GetJsonAsync<UserInfo> ("user");

			var identity = userInfo.IsAuthenticated ?
				new ClaimsIdentity (new [] { new Claim (ClaimTypes.Name, userInfo.Name) }, "serverauth") :
				new ClaimsIdentity ();

			return new AuthenticationState (new ClaimsPrincipal (identity));
		}
	}
}