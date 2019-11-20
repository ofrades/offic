using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Shared;
using System.Collections.Generic;

namespace Server {

	/// <summary>
	/// Github Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Get File Controller with path
		/// </summary>
		/// <returns>RepoFile</returns>
		[Route("file/{owner}/{repoName}/{**path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFile>> GetFileContent(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContentsByRef(owner, repoName, path, "master"))
				.Select(r => new RepoFile(r.Content, r.Name));

			return result;
		}
	}
}