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
	/// GetRepos Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// GetRepos
		/// </summary>
		/// <returns>GitRepo</returns>
		[Route("{owner}/repos")]
		[HttpGet]
		public async Task<IEnumerable<Repo>> GetRepo(
			[FromRoute] string owner
		) {
			var client = await NewClient();
			var repos = (await client.Repository.GetAllForUser(owner))
				.Where(r => r.Name != "")
				.OrderByDescending(r => r.StargazersCount)
				.Select(r => new Repo (r.Name, r.FullName, r.Description, r.StargazersCount))
				.Take(10);

			return repos;
		}
	}
}