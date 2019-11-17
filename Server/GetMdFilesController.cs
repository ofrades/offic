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
	/// GetMdFiles Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetMdFilesController : Controller {

		/// <summary>
		/// Get Markdown Files
		/// </summary>
		/// <returns></returns>
		[Route("{search}/mdfiles")]
		[HttpGet]
		public async Task<SearchCodeResult> GetMdFiles(
			[FromRoute] string search
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();

			var request = new SearchCodeRequest("auth"){
				Language = Language.Markdown,
				FileName = search
			};

			var result = await client.Search.SearchCode(request);

			return result;
		}
	}
}