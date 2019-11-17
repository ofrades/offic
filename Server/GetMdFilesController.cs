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
	public partial class GitHubController : Controller {

		/// <summary>
		/// Get Markdown Files
		/// </summary>
		/// <returns></returns>
		[Route("{search}/mdfiles")]
		[HttpGet]
		public async Task<SearchCodeResult> GetMdFiles(
			[FromRoute] string search
		) {
			var client = await NewClient();

			var request = new SearchCodeRequest("auth"){
				Language = Language.Markdown,
				FileName = search
			};

			var result = await client.Search.SearchCode(request);

			return result;
		}
	}
}