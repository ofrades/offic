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
	/// GetRepoDirFiles Controller
	/// </summary>

	[Route("api")]
	[ApiController]
	[Authorize]
	public partial class GetRepoDirFilesController : Controller {

		/// <summary>
		/// Get Repo Directory Files
		/// </summary>
		/// <returns></returns>
		[Route("{owner}/{repoName}/{search}")]
		[HttpGet]
		public async Task<IEnumerable<string>> GetRepoDirFiles(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string search
		) {
			var newClient = new CreateClient();
			var client = await newClient.NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName, search))
				.Where(r => r.Name != "")
				.OrderByDescending(r => r.Name)
				.Select(r => r.Name);

			return result;
		}
	}
}