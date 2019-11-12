using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Shared {

	/// <summary>
	/// MostStarred
	/// </summary>
	public partial class MostStarred {

		/// <summary>
		/// New Repo User
		/// </summary>
		/// <value></value>
		public string ownerMostStarred = "";

		/// <summary>
		/// Files
		/// </summary>
		public IEnumerable<GitRepo> stringJson;

		/// <summary>
		/// Inject HttpClient
		/// </summary>
		/// <value></value>
		[Inject]
		public HttpClient HttpClient { get; set; }

		/// <summary>
		/// GetMostStarredFiles
		/// </summary>
		/// <returns></returns>
		public async Task GetMostStarredFiles(){
			var apiUrl = $"api/{ownerMostStarred}/mostfiles";
			stringJson = await HttpClient.GetJsonAsync<IEnumerable<GitRepo>>(apiUrl);
		}

		/// <summary>
		/// StarredRepos
		/// </summary>
		/// <value></value>
		public IEnumerable<GitRepo> StarredRepos { get; set; } = new GitRepo[0];

		/// <summary>
		/// GithubMdService
		/// </summary>
		/// <value></value>
		[Inject]
		public IMdService GithubMdService { get; set; }

		/// <summary>
		/// SetHomeContentAsync
		/// </summary>
		/// <returns></returns>
		public async Task SetHomeContentAsync () {

			StarredRepos = await GithubMdService.GetMostStarredRepos (ownerMostStarred);
		}
	}
}