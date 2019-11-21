using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Octokit;
using System.Linq;

namespace Server.Controllers {

	/// <summary>
	/// DeleteFile Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Delete File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
        /// <param name="message"></param>
		/// <param name="path"></param>
		[Route("delete/{owner}/{repoName}/{message}/{**path}")]
		[HttpDelete]
		public async Task DeleteFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
            [FromRoute] string message,
			[FromRoute] string path

		) {
			var client = await NewClient();
			var repository = await client.Repository.Get(owner, repoName);

			var existingFile = (await client.Repository.Content.GetAllContentsByRef(
				owner,
				repository.Name,
				path,
				repository.DefaultBranch))
					.FirstOrDefault();

			await client.Repository.Content.DeleteFile(
				owner,
				repoName,
				path,
				new DeleteFileRequest(
					message,
					existingFile.Sha));
		}
	}
}