using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Octokit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Server.Controllers {

	/// <summary>
	/// Get Api Limits Controller and Create New Client
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public class GetApiLimitController : Controller {

		private readonly AuthorizeClient _authorizeClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authorizeClient"></param>
		public GetApiLimitController(
			AuthorizeClient authorizeClient
		) {
			_authorizeClient = authorizeClient;
		}
		/// <summary>
		/// Get Rate Limits
		/// </summary>
		/// <returns>string</returns>
		[Route("limits")]
		[HttpGet]
		public async Task<string> GetApiLimit() {
			var client = await _authorizeClient.Authorize();
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