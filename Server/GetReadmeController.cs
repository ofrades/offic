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
	/// GetReadme Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetReadmeController : Controller {

		/// <summary>
		/// Get Readme
		/// </summary>
		/// <returns>GitReadme</returns>
		[Route("{owner}/{repoName}/readme")]
		[HttpGet]
		public async Task<RepoReadme> GetReadme(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();
			var readme = await client.Repository.Content.GetReadme(owner, repoName);

			var gitReadme = new RepoReadme {
				readmeContent = readme.Content,
				readmeName = readme.Name,
			};
			return gitReadme;
		}
	}
}