using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Octokit;

namespace Server {

	/// <summary>
	/// CreateFile Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Create File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <param name="path"></param>
		/// <param name="content"></param>
		[Route("create/{owner}/{repoName}/{**path}")]
		[HttpPost]
		public async Task CreateFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromBody] string content,
			[FromRoute] string path = "NewFile.md"
		) {
			var client = await NewClient();
			var repository = await client.Repository.Get(owner, repoName);
			var defaultBranchName = repository.DefaultBranch;
			
			var createFile = client.Repository.Content.CreateFile(
				owner,
				repoName,
				path,
				new CreateFileRequest(
					"File Create",
					content,
					defaultBranchName,
					true
				)
			);
		}
	}
}