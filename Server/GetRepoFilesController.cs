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
	/// GetRepoFiles Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetRepoFilesController : Controller {

		/// <summary>
		/// Get Repo Files
		/// </summary>
		/// <returns></returns>
		[Route("{owner}/{repoName}/files")]
		[HttpGet]
		public async Task<IEnumerable<string>> GetRepoFiles(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner,repoName))
				.Where(r => r.Name != "")
				.OrderByDescending(r => r.Name)
				.Select(r => r.Name);

			return result;
		}
	}
}