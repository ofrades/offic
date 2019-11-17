using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;

namespace Client.Components {

	public partial class ListFiles {

		[Parameter]
		public string owner { get; set; }

		[Parameter]
		public string repoName { get; set; }

		[Parameter]
		public string path { get; set; }

		[Parameter]
		public string content { get; set; }

		[Inject]
		HttpClient HttpClient { get; set; }

		public bool showEditor = false;
		public string _fileContent;
		public string _fileName;
		public List<RepoListFiles> _reposList;
		public List<RepoFileContent> _repoFileContent;

		async Task GetRepoFiles() {
			var apiUrl = $"api/{owner}/{repoName}/files";
			_reposList = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrl);
		}

		public async Task GetFile(string path) {
			showEditor = false;
			var apiUrl = $"api/{owner}/{repoName}/{path}/file";
			_repoFileContent = await HttpClient.GetJsonAsync<List<RepoFileContent>>(apiUrl);
			showEditor = true;
		}

		public async Task UpdateFile(string path) {
			var apiUrl = $"api/{owner}/{repoName}/{path}/update";
			try {
				// IsLoading = true;
				await HttpClient.PostJsonAsync<string>(apiUrl, content);	
			}
			catch (System.Exception) {
				
				throw;
			}
			Console.WriteLine("Success");
		}
	}
}