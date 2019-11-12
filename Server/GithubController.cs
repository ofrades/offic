using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Octokit;
using Shared;
using Server.Models;
using Microsoft.AspNetCore.Authorization;

namespace Server {

	/// <summary>
	/// Github Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public class GithubController : Controller {
		private GitReadme _gitReadme;

		/// <summary>
		/// GithubController Constructor
		/// </summary>
		/// <param name="gitReadme"></param>
		public GithubController(GitReadme gitReadme){
			_gitReadme = gitReadme;
		}

		/// <summary>
		/// Create Client
		/// </summary>
		/// <returns></returns>
		public async Task<GitHubClient> CreateClient() {
			var authResult = await HttpContext.AuthenticateAsync();
			if (!authResult.Succeeded) {
				throw new Exception("Not Authenticated");
			}
			var accessToken = authResult.Properties.Items[".Token.access_token"];
			var client = new GitHubClient(new ProductHeaderValue("offic"));
			client.Credentials = new Credentials(accessToken);
			return client;
		}
		UpdateForm updatedContent = new UpdateForm();
		/// <summary>
		/// Update File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <param name="updatedContent"></param>
		/// <returns></returns>
		[Route("{owner}/{repoName}/update")]
		[HttpPost]
		public async Task UpdateFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromForm] string updatedContent
		) {
			var client = await CreateClient();
			var repository = await client.Repository.Get(owner, repoName);
			var defaultBranchName = repository.DefaultBranch;
			var lastCommit = (await client.Repository.Commit.GetAll(owner, repoName))
				.FirstOrDefault();

			var updateFile = await client.Repository.Content.UpdateFile(owner, repoName, "api/{owner}/{repoName}/update", new UpdateFileRequest("updatefile", updatedContent, lastCommit.Sha.ToString()));
		}

		/// <summary>
		/// RateLimits
		/// </summary>
		/// <returns></returns>
		[Route("limits")]
		[HttpGet]
		public async Task<ActionResult<string>> GetApiLimit() {
			var client = await CreateClient();
            var limits = await client.Miscellaneous.GetRateLimits();
            var rateLimits =  String.Format(
                "Rate(Limit:{0}, Remaining:{1}), Core(Limit:{2}, Remaining:{3}), Search(Limits:{4}, Remaining:{5})",
                limits.Rate.Limit, limits.Rate.Remaining,
                limits.Resources.Core.Limit, limits.Resources.Core.Remaining,
                limits.Resources.Search.Limit, limits.Resources.Search.Remaining);
			return Ok(rateLimits);
        }

		/// <summary>
		/// Create File
		/// </summary>
		/// <param name="data"></param>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <returns></returns>
		[Route("{owner}/{repoName}/create")]
		[HttpGet]
		public async Task<IActionResult> CreateFile(
			[FromBody] RepoUpdateRequest data,
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await CreateClient();
			var repository = await client.Repository.Get(owner, repoName);
			var defaultBranchName = repository.DefaultBranch;
			
			var createFile = client.Repository.Content.CreateFile(owner, repoName, "/README.md", new CreateFileRequest("File Create", data.Content, defaultBranchName, true));

			return Created("/create", createFile);
		}

		/// <summary>
		/// Delete File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <returns></returns>
		[Route("{owner}/{repoName}/delete")]
		[HttpGet]
		public async Task DeleteFile(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await CreateClient();
			var repository = await client.Repository.Get(owner, repoName);
			var defaultBranchName = repository.DefaultBranch;

			await client.Repository.Content.DeleteFile(
				owner,repoName, "/README.md",
				new DeleteFileRequest("File Delete", defaultBranchName));
		}

		/// <summary>
		/// Get Readme
		/// </summary>
		/// <returns></returns>
		[Route("{owner}/{repoName}/readme")]
		[HttpGet]
		public async Task<GitReadme> GetReadme(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await CreateClient();
			var readme = await client.Repository.Content.GetReadme(owner, repoName);
			_gitReadme.readmeContent = readme.Content;
			_gitReadme.readmeName = readme.Name;
			return _gitReadme;
		}
	}
}