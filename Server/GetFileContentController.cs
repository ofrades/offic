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
	public partial class GitHubController : Controller {

		/// <summary>
		/// GetFile Controller
		/// </summary>
		/// <returns></returns>
		[Route("file/{owner}/{repoName}/{**path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFileContent>> GetFileContent(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
			
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContentsByRef(owner, repoName, path, "master"))
				.Select(r => new RepoFileContent(r.Content, r.Name));

			return result;
		}
	}
}