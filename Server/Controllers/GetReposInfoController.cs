using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Shared;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers {

	/// <summary>
	/// Get Repo Info Controller
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public class GetReposInfoController : Controller {

		private readonly AuthorizeClient _authorizeClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authorizeClient"></param>
		public GetReposInfoController(
			AuthorizeClient authorizeClient
		) {
			_authorizeClient = authorizeClient;
		}

		/// <summary>
		/// Get Repo Info Controller
		/// </summary>
		/// <returns>GitRepo</returns>
		[Route("repos/{owner}")]
		[HttpGet]
		public async Task<IEnumerable<ReposInfo>> GetRepo(
			[FromRoute] string owner = ""
		) {
			var client = await _authorizeClient.Authorize();
			var repos = (await client.Repository.GetAllForUser(owner))
				.Where(r => r.Name == r.Name)
				.OrderByDescending(r => r.StargazersCount)
				.Select(r => new ReposInfo (r.Name, r.FullName, r.Description, r.StargazersCount))
				.Take(10);

			return repos;
		}
	}
}