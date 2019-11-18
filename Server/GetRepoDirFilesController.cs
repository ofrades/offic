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
	/// GetRepoDirFiles Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Get Repo Directory Files
		/// </summary>
		/// <returns></returns>
		[Route("{owner}/{repoName}/{path}/dir")]
		[HttpGet]
		public async Task<IEnumerable<RepoDirFiles>> GetRepoDirFiles(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName, path))
				.Where(r => r.Name != "")
				.OrderByDescending(r => r.Name)
				.Select(r => new RepoDirFiles(r.Name));

			return result;
		}
	}
}