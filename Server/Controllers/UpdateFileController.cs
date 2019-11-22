using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Octokit;
using Html2Markdown;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers {

	/// <summary>
	/// UpdateFile Controller
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public class UpdateFileController : Controller {

		private readonly AuthorizeClient _authorizeClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authorizeClient"></param>
		public UpdateFileController(
			AuthorizeClient authorizeClient
		) {
			_authorizeClient = authorizeClient;
		}

		/// <summary>
		/// Update File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
        /// <param name="message"></param>
		/// <param name="path"></param>
		/// <param name="content"></param>
		[Route("update/{owner}/{repoName}/{message}/{**path}")]
		[HttpPost]
		public async Task UpdateFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
            [FromRoute] string message,
			[FromRoute] string path,
			[FromBody] string content
		) {
			var client = await _authorizeClient.Authorize();
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

			var converter = new Converter();
			var updatedContent = converter.Convert(content);

			var updateFile = await client.Repository.Content.UpdateFile(
				owner,
				repoName,
				path,
				new UpdateFileRequest(
					message,
					updatedContent,
					existingFile.Sha,
					defaultBranchName
				)
			);
		}
	}
}