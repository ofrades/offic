using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Octokit;

namespace Server.Controllers {

	/// <summary>
	/// UpdateFile Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Update File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <param name="path"></param>
		/// <param name="updatedContent"></param>
		[Route("update/{owner}/{repoName}/{**path}")]
		[HttpPost]
		public async Task UpdateFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path,
			[FromBody] string updatedContent
		) {
			var client = await NewClient();
			var repository = await client.Repository.Get(owner, repoName);

			var repositoryId = repository.Id;
			var defaultBranchName = repository.DefaultBranch;
			var existingFile = (await client.Repository.Content.GetAllContentsByRef(
				owner,
				repository.Name,
				path,
				repository.DefaultBranch))
					.FirstOrDefault();

			if (existingFile == null) {
				throw new ArgumentException("Parameter cannot be null");
			}

			var updateFile = await client.Repository.Content.UpdateFile(
				owner,
				repoName,
				path,
				new UpdateFileRequest(
					"Success: Updated File",
					updatedContent,
					existingFile.Sha,
					defaultBranchName
				)
			);
		}
	}
}