using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.Controllers {

	/// <summary>
	/// GetReadme Controller
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public class GetReadmeController : Controller {

		private readonly AuthorizeClient _authorizeClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authorizeClient"></param>
		public GetReadmeController(
			AuthorizeClient authorizeClient
		) {
			_authorizeClient = authorizeClient;
		}

		/// <summary>
		/// Get Main Readme File
		/// </summary>
		/// <returns>GitReadme</returns>
		[Route("{owner}/{repoName}/readme")]
		[HttpGet]
		public async Task<RepoReadme> GetReadme(
			[FromRoute] string owner, [FromRoute] string repoName
		) {
			var client = await _authorizeClient.Authorize();
			var readme = await client.Repository.Content.GetReadme(owner, repoName);

			var gitReadme = new RepoReadme {
				Content = readme.Content,
					Name = readme.Name,
			};
			return gitReadme;
		}
	}
}