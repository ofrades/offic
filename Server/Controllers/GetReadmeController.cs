using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Shared;

namespace Server {

	/// <summary>
	/// GetReadme Controller
	/// </summary>

	public partial class GitHubController : Controller {

		/// <summary>
		/// Get Main Readme File
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