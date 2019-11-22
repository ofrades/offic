using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Octokit;
using Html2Markdown;
using Shared.Validation;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers {

	/// <summary>
	/// CreateFile Controller
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public class CreateFileController : Controller {

		private readonly AuthorizeClient _authorizeClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authorizeClient"></param>
		public CreateFileController(
			AuthorizeClient authorizeClient
		) {
			_authorizeClient = authorizeClient;
		}

		/// <summary>
		/// Create File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <param name="message"></param>
		/// <param name="path"></param>
		/// <param name="content"></param>
		[Route("create/{owner}/{repoName}/{message}/{**path}")]
		[HttpPost]
		public async Task CreateFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
            [FromRoute] string message,
			[FromRoute] string path,
			[FromBody] string content
		) {
			var client = await _authorizeClient.Authorize();
			var repository = await client.Repository.Get(owner, repoName);
			var defaultBranchName = repository.DefaultBranch;
			var converter = new Converter();
			var createdContent = converter.Convert(content);

			var createFile = client.Repository.Content.CreateFile(
				owner,
				repoName,
				path,
				new CreateFileRequest(
					message,
					createdContent,
					defaultBranchName,
					true
				)
			);
		}
	}
}