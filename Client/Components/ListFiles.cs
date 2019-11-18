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
		public bool cleanList = false;
		public string _fileContent;
		public string _fileName;
		public List<RepoListFiles> _reposList;
		public List<RepoListFiles> _reposFolders;
		public List<RepoDirFiles> _repoDirFiles;
		public List<RepoFileContent> _repoFileContent;

		async Task GetRepoFiles() {
			var apiUrlFiles = $"api/files/{owner}/{repoName}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFolders);
			Reset();
		}

		async Task<string> GetRepoFilesPath(string _path) {
			path += $"{_path}/";

			var apiUrlFiles = $"api/files/{owner}/{repoName}/{path}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}/{path}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoListFiles>>(apiUrlFolders);
			cleanList = true;
			return path;
		}

		public async Task<string> GetFile(string _path) {
			path += $"{_path}/";
			showEditor = false;
			var apiUrl = $"api/file/{owner}/{repoName}/{path}";
			_repoFileContent = await HttpClient.GetJsonAsync<List<RepoFileContent>>(apiUrl);
			showEditor = true;
			return path;
		}

		public void Reset(){
			showEditor = false;
			cleanList = true;
			path = "";
		}

		public async Task UpdateFile() {
			var apiUrl = $"api/update/{owner}/{repoName}/{path}";
			try {
				await HttpClient.PostJsonAsync<string>(apiUrl, content);	
			}
			catch (System.Exception) {
				throw;
			}
			path = "";
		}
	}
}