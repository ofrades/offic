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
	/// UpdateFile Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class UpdateFileController : Controller {

		/// <summary>
		/// Update File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <param name="updatedContent"></param>
		[Route("{owner}/{repoName}/update")]
		[HttpPost]
		public async Task UpdateFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromBody] string updatedContent
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();
			var repository = await client.Repository.Get(owner, repoName);

			var repositoryId = repository.Id;
			var defaultBranchName = repository.DefaultBranch;
			
			var existingFile = (await client.Repository.Content.GetAllContentsByRef(owner, repository.Name, "README.md", repository.DefaultBranch)).FirstOrDefault();

			if (existingFile == null) {
				throw new ArgumentException("Parameter cannot be null");
			}

			var updateFile = await client.Repository.Content.UpdateFile(
				owner,
				repoName,
				existingFile.Path,
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