using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Octokit;
using Shared;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace Server
{

	/// <summary>
	/// GetApiLimit Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetApiLimitController : Controller {

		/// <summary>
		/// RateLimits
		/// </summary>
		/// <returns>string</returns>
		[Route("limits")]
		[HttpGet]
		public async Task<ActionResult<string>> GetApiLimit() {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();
            var limits = await client.Miscellaneous.GetRateLimits();
            var rateLimits =  String.Format(
                "Rate(Limit:{0}, Remaining:{1}), Core(Limit:{2}, Remaining:{3}), Search(Limits:{4}, Remaining:{5})",
                limits.Rate.Limit, limits.Rate.Remaining,
                limits.Resources.Core.Limit, limits.Resources.Core.Remaining,
                limits.Resources.Search.Limit, limits.Resources.Search.Remaining);
			return Ok(rateLimits);
        }
	}
}