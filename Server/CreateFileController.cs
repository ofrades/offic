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
	/// CreateFile Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class CreateFileController : Controller {

		/// <summary>
		/// Create File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		/// <param name="title"></param>
		/// <param name="content"></param>
		[Route("{owner}/{repoName}/{title}/create")]
		[HttpPost]
		public async Task CreateFile(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string title,
			[FromBody] string content
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();
			var repository = await client.Repository.Get(owner, repoName);
			var defaultBranchName = repository.DefaultBranch;
			
			var createFile = client.Repository.Content.CreateFile(
				owner,
				repoName,
				title,
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