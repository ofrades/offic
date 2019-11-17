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
	/// GetTxtFiles Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetTxtFilesController : Controller {

		/// <summary>
		/// Get Text Files
		/// </summary>
		/// <returns></returns>
		[Route("{search}/txtfiles")]
		[HttpGet]
		public async Task<SearchCodeResult> GetTxtFiles(
			[FromRoute] string search
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();

			var request = new SearchCodeRequest("auth"){
				Language = Language.Textile,
				FileName = search
			};

			var result = await client.Search.SearchCode(request);

			return result;
		}
	}
}