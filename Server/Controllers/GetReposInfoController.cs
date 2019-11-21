using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Shared;
using System.Collections.Generic;

namespace Server.Controllers {

	/// <summary>
	/// Get Repo Info Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Get Repo Info Controller
		/// </summary>
		/// <returns>GitRepo</returns>
		[Route("repos/{owner}")]
		[HttpGet]
		public async Task<IEnumerable<ReposInfo>> GetRepo(
			[FromRoute] string owner = ""
		) {
			var client = await NewClient();
			var repos = (await client.Repository.GetAllForUser(owner))
				.Where(r => r.Name == r.Name)
				.OrderByDescending(r => r.StargazersCount)
				.Select(r => new ReposInfo (r.Name, r.FullName, r.Description, r.StargazersCount))
				.Take(10);

			return repos;
		}
	}
}