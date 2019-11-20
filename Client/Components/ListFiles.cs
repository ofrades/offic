using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;

namespace Client.Components
{

	public partial class ListFiles {

		[Parameter]
		public string owner { get; set; }

		[Parameter]
		public string repoName { get; set; }

		[Parameter]
		public string content { get; set; }

		[Parameter]
		public string newContent { get; set; }

		[Parameter]
		public string path { get; set; }

		[Inject]
		HttpClient HttpClient { get; set; }

		public bool showEditor = false;
		public bool showEditorNewFile = false;
		public bool showDone = false;
		public bool showList = false;
		public string _fileContent;
		public string _fileName;
		List<RepoFile> _reposList;
		List<RepoFile> _reposFolders;
		List<RepoFile> _repoFileContent;

		List<ReposInfo> _reposListStars;
		List<string> _urlList = new List<string>();
		string _urlListPath;

		async Task GetRepos() {
			showList = false;
			var apiUrl = $"api/repos/{owner}";
			_reposListStars = await HttpClient.GetJsonAsync<List<ReposInfo>>(apiUrl);
		}

		async Task<string> GetRepoFiles(string _repoName) {
			repoName = _repoName;
			showDone = false;
			_reposListStars = null;
			var apiUrlFiles = $"api/files/{owner}/{repoName}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
			showList = true;
			return repoName;
		}

		void ListClose() {
			showList = false;
			_urlList.Clear();
			_urlListPath = "";
		}

		void EditorClose() {
			showEditor = false;
			showEditorNewFile = false;
			PreviousFolder().GetAwaiter();
		}

		async Task<List<string>> GetRepoFilesPath(string _path) {
			_urlList.Add(_path);
			_urlListPath = String.Join("/", _urlList);

			// path += $"{_path}/";
			_reposListStars = null;
			var apiUrlFiles = $"api/files/{owner}/{repoName}/{_urlListPath}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}/{_urlListPath}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
			return _urlList;
		}

		async Task PreviousFolder(){
			showEditor = false;
			showEditorNewFile = false;
			showDone = false;
			_urlList.RemoveAt(_urlList.Count - 1);
			_urlListPath = String.Join("/", _urlList);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}/{_urlListPath}";
			var apiUrlFiles = $"api/files/{owner}/{repoName}/{_urlListPath}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
		}

		async Task<List<string>> GetFile(string _path) {
			_urlList.Add(_path);
			_urlListPath = String.Join("/", _urlList);
			showEditor = false;
			showDone = false;
			_reposListStars = null;
			var apiUrl = $"api/file/{owner}/{repoName}/{_urlListPath}";
			_repoFileContent = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrl);
			showEditor = true;
			return _urlList;
		}

		void UpdateFile() {
			var apiUrl = $"api/update/{owner}/{repoName}/{_urlListPath}";
			HttpClient.PostJsonAsync<string>(apiUrl, content);
			showEditor = false;
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		async Task DeleteFile() {
			var apiUrl = $"api/delete/{owner}/{repoName}/{_urlListPath}";
			await HttpClient.DeleteAsync(apiUrl);
			showEditor = false;
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		void ShowEditorNewFile() {
			showDone = false;
			showEditorNewFile = true;
		}

		void CreateFile() {
			_urlList.Add("NewFile.md");
			_urlListPath = String.Join("/", _urlList);
			var apiUrl = $"api/create/{owner}/{repoName}/{_urlListPath}";
			HttpClient.PostJsonAsync<string>(apiUrl, newContent);
			showEditorNewFile = false;
			PreviousFolder().GetAwaiter();
			showDone = true;
		}
	}
}