using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Octokit;
using Shared;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IO;

namespace Server
{

	/// <summary>
	/// GetRepoFiles Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Get Repo Files
		/// </summary>
		/// <returns></returns>
		[Route("{owner}/{repoName}/files")]
		[HttpGet]
		public async Task<IEnumerable<RepoListFiles>> GetRepoFilesList(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner,repoName))
				.Where(s => new[] { ".md", ".txt" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoListFiles(r.Name));

			return result;
		}
	}
}
