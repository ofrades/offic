using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Shared;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Server.Controllers {

	/// <summary>
	/// GetRepoFile Controller
	/// </summary>
	[Route("api")]
	[ApiController]
	[Authorize]
	public class GetRepoFilesListController : Controller {
		private readonly AuthorizeClient _authorizeClient;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="authorizeClient"></param>
		public GetRepoFilesListController(
			AuthorizeClient authorizeClient
		) {
			_authorizeClient = authorizeClient;
		}

		/// <summary>
		/// Get Repo Files List with path
		/// </summary>
		/// <returns>RepoFile</returns>
		[Route("files/{owner}/{repoName}/{**path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFile>> GetRepoFilesListPath(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
		) {
			var client = await _authorizeClient.Authorize();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName, path))
				.Where(s => new[] { ".md", ".txt" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoFile(r.Name));

			return result;
		}

		/// <summary>
		/// Get Repo Folders with path
		/// </summary>
		/// <returns>RepoFile</returns>
		[Route("folders/{owner}/{repoName}/{**path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFile>> GetRepoFoldersListPath(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
		) {
			var client = await _authorizeClient.Authorize();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName, path))
				.Where(s => new[] { "" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoFile(r.Name));

			return result;
		}

		/// <summary>
		/// Get Repo File List without path
		/// </summary>
		/// <returns>RepoFile</returns>
		[Route("files/{owner}/{repoName}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFile>> GetRepoFilesList(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await _authorizeClient.Authorize();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName))
				.Where(s => new[] { ".md", ".txt" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoFile(r.Name));

			return result;
		}

		/// <summary>
		/// Get Repo Folders List without path
		/// </summary>
		/// <returns>RepoFile</returns>
		[Route("folders/{owner}/{repoName}")]
		[HttpGet]
		public async Task<IEnumerable<RepoFile>> GetRepoFoldersList(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await _authorizeClient.Authorize();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName))
				.Where(s => new[] { "" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoFile(r.Name));

			return result;
		}
	}
}
