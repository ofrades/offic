using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace Server.Controllers {

	/// <summary>
	/// Github Controller
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public class GetFileContentController : Controller {

		private readonly AuthorizeClient _authorizeClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authorizeClient"></param>
		public GetFileContentController(
			AuthorizeClient authorizeClient
		) {
			_authorizeClient = authorizeClient;
		}

		/// <summary>
		/// Get File Controller with path
		/// </summary>
		/// <returns>RepoFile</returns>
		[Route("file/{owner}/{repoName}/{**path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFile>> GetFileContent(
			[FromRoute] string owner, [FromRoute] string repoName, [FromRoute] string path
		) {
			var client = await _authorizeClient.Authorize();

			var result = (await client.Repository.Content.GetAllContentsByRef(owner, repoName, path, "master"))
				.Select(r => new RepoFile(r.Content, r.Name));

			return result;
		}
	}
}