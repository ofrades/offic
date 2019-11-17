using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;

namespace Client.Components {

	public partial class Search {

		[Parameter]
		string owner { get; set; }

		[Parameter]
		string repoName { get; set; }

		[Parameter]
		string path { get; set; }

		[Inject]
		HttpClient HttpClient { get; set; }

		List<RepoListFiles> _reposList;
		List<RepoFile> _repoFile;

		async Task GetRepoFiles() {
			// IsLoading = true;
			var apiUrl = $"api/{owner}/{repoName}/files";
			_reposList = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrl);
		}

		async Task GetFile() {
			// IsLoading = true;
			var apiUrl = $"api/{owner}/{repoName}/file/{path}";
			_repoFile = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrl);
		}
	}
}