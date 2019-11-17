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

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetReposController : Controller {

		/// <summary>
		/// GetRepos
		/// </summary>
		/// <returns>GitRepo</returns>
		[Route("{owner}/repos")]
		[HttpGet]
		public async Task<IEnumerable<Repo>> GetRepos(
			[FromRoute] string owner
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();
			var repos = (await client.Repository.GetAllForUser(owner))
				.Where(r => r.Name != "")
				.OrderByDescending(r => r.StargazersCount)
				.Select(r => new Repo (r.Name, r.FullName, r.Description, r.StargazersCount))
				.Take(10);

			return repos;
		}
	}
}