using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Client.Components {

	public partial class ListFiles {

		[Parameter]
		public string owner { get; set; }

		[Parameter]
		public string repoName { get; set; }

		[Parameter]
		public string content { get; set; }

		[Parameter]
		public string path { get; set; }

		[Inject]
		HttpClient HttpClient { get; set; }

		public bool showEditor = false;
		public bool showDone = false;
		public bool showList = false;
		public string _fileContent;
		public string _fileName;
		public List<RepoListFiles> _reposList;
		public List<RepoListFiles> _reposFolders;
		public List<RepoFileContent> _repoFileContent;

		public List<Repo> _reposListStars;

		public async Task GetRepos() {
			showList = false;
			var apiUrl = $"api/{owner}/repos";
			_reposListStars = await HttpClient.GetJsonAsync<List<Repo>>(apiUrl);
		}

		async Task<string> GetRepoFiles(string _repoName) {
			repoName = _repoName;
			var apiUrlFiles = $"api/files/{owner}/{repoName}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFolders);
			showDone = false;
			showList = true;
			_reposListStars = null;
			return repoName;
		}

		void ListClose(){
			showList = false;
			path = "";
		}
		void EditorClose(){
			showEditor = false;
			path = "";
		}

		async Task<string> GetRepoFilesPath(string _path) {
			path += $"{_path}/";
			var apiUrlFiles = $"api/files/{owner}/{repoName}/{path}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}/{path}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFolders);
			_reposListStars = null;
			return path;
		}

		public async Task<string> GetFile(string _path) {
			path += $"{_path}/";
			showEditor = false;
			var apiUrl = $"api/file/{owner}/{repoName}/{path}";
			_repoFileContent = await HttpClient.GetJsonAsync<List<RepoFileContent>>(apiUrl);
			showEditor = true;
			_reposListStars = null;
			return path;
		}

		public async Task UpdateFile() {
			showDone = true;
			showList = false;
			showEditor = false;
			var apiUrl = $"api/update/{owner}/{repoName}/{path}";
			await HttpClient.PostJsonAsync<string>(apiUrl, content);
			_reposListStars = null;
			path = "";
		}

		public async Task CreateFile() {
			var apiUrl = $"api/create/{owner}/{repoName}/{path}";
			await HttpClient.PostJsonAsync<string>(apiUrl, content);
		}
	}
}