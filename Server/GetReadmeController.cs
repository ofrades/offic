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

	public partial class GitHubController : Controller {

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
			var client = await NewClient();
			var readme = await client.Repository.Content.GetReadme(owner, repoName);

			var gitReadme = new RepoReadme {
				Content = readme.Content,
				Name = readme.Name,
			};
			return gitReadme;
		}
	}
}