using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Octokit;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers {

	/// <summary>
	/// Get Api Limits Controller and Create New Client
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GitHubController : Controller {

		/// <summary>
		/// Create New Client
		/// </summary>
		/// <returns>GitHubClient</returns>
		public async Task<GitHubClient> NewClient() {
			var authResult = await HttpContext.AuthenticateAsync();
			if (!authResult.Succeeded) {
				throw new Exception("Not Authenticated");
			}
			var accessToken = authResult.Properties.Items[".Token.access_token"];
			var client = new GitHubClient(new ProductHeaderValue("offic"));
			client.Credentials = new Credentials(accessToken);
			return client;
		}

		/// <summary>
		/// Get Rate Limits
		/// </summary>
		/// <returns>string</returns>
		[Route("limits")]
		[HttpGet]
		public async Task<string> GetApiLimit() {
			var client = await NewClient();
            var limits = await client.Miscellaneous.GetRateLimits();
            var rateLimits =  String.Format(
                "Rate(Limit:{0}, Remaining:{1}), Core(Limit:{2}, Remaining:{3}), Search(Limits:{4}, Remaining:{5})",
                limits.Rate.Limit,
				limits.Rate.Remaining,
                limits.Resources.Core.Limit,
				limits.Resources.Core.Remaining,
                limits.Resources.Search.Limit,
				limits.Resources.Search.Remaining);
			return rateLimits;
        }
	}
}