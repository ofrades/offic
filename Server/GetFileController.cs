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
	/// Github Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetFileController : Controller {

		/// <summary>
		/// GetFile Controller
		/// </summary>
		/// <returns></returns>
		[Route("{owner}/{repoName}/file/{path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFile>> GetFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
			
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();

			var result = (await client.Repository.Content.GetAllContentsByRef(owner, repoName, path))
				.Select(r => new RepoFile(r.Content));

			return result;
		}
	}
}