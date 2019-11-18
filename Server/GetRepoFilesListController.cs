using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Octokit;
using Shared;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.IO;

namespace Server
{

	/// <summary>
	/// GetRepoFiles Controller
	/// </summary>
	public partial class GitHubController : Controller {

		/// <summary>
		/// Get Repo Files
		/// </summary>
		/// <returns></returns>
		[Route("files/{owner}/{repoName}/{**path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoListFiles>> GetRepoFilesListPath(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName, path))
				.Where(s => new[] { ".md", ".txt" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoListFiles(r.Name));

			return result;
		}

		/// <summary>
		/// Get Repo Files
		/// </summary>
		/// <returns></returns>
		[Route("folders/{owner}/{repoName}/{**path}")]
		[HttpGet]
		public async Task<IEnumerable<RepoListFiles>> GetRepoFoldersListPath(
			[FromRoute] string owner,
			[FromRoute] string repoName,
			[FromRoute] string path
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName, path))
				.Where(s => new[] { "" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoListFiles(r.Name));

			return result;
		}

		/// <summary>
		/// Get Repo Files
		/// </summary>
		/// <returns></returns>
		[Route("files/{owner}/{repoName}")]
		[HttpGet]
		public async Task<IEnumerable<RepoListFiles>> GetRepoFilesList(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName))
				.Where(s => new[] { ".md", ".txt" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoListFiles(r.Name));

			return result;
		}

		/// <summary>
		/// Get Repo Files
		/// </summary>
		/// <returns></returns>
		[Route("folders/{owner}/{repoName}")]
		[HttpGet]
		public async Task<IEnumerable<RepoListFiles>> GetRepoFoldersList(
			[FromRoute] string owner,
			[FromRoute] string repoName
		) {
			var client = await NewClient();

			var result = (await client.Repository.Content.GetAllContents(owner, repoName))
				.Where(s => new[] { "" }
				.Any(e => e == Path.GetExtension(s.Name)))
				.OrderBy(o => o.Name)
				.Select(r => new RepoListFiles(r.Name));

			return result;
		}
	}
}
