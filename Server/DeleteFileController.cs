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
	/// DeleteFile Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Delete File
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="repoName"></param>
		[Route("{owner}/{repoName}/delete")]
		[HttpGet]
		public async Task DeleteFile(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await NewClient();
			var repository = await client.Repository.Get(owner, repoName);
			var defaultBranchName = repository.DefaultBranch;

			await client.Repository.Content.DeleteFile(
				owner,
				repoName,
				"/README.md",
				new DeleteFileRequest(
					"File Delete",
					defaultBranchName));
		}
	}
}