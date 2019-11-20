using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Shared;
using System.Collections.Generic;
using System.IO;

namespace Server {

	/// <summary>
	/// GetRepoFile Controller
	/// </summary>
	public partial class GitHubController : Controller {

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
			var client = await NewClient();

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
			var client = await NewClient();

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
			var client = await NewClient();

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
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName))
				.Where(s => new[] { "" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoFile(r.Name));

			return result;
		}
	}
}
