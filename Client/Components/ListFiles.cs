using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared.Models;
using Shared.Validation;

namespace Client.Components {

	public partial class ListFiles {
		[Inject]
		HttpClient HttpClient { get; set; }

		[Required]
		public string owner { get; set; }
		public string repoName { get; set; }

		[Required]
		public string newContent { get; set; }

		[Required]
		public string commitMessage { get; set; }

		[Required]
		public string newFileName { get; set; }

		[Required]
		public string newFileExtension { get; set; }

		[Required]
		public string content { get; set; }
		bool showEditor = false;
		bool showEditorNewFile = false;
		bool showDone = false;
		bool showList = false;
		bool IsFile = false;

		UpdateContent UpdateContent { get; set; } = new UpdateContent();
		NewContent NewContent { get; set; } = new NewContent();
		List<RepoFile> reposList;
		List<RepoFile> reposFolders;
		List<RepoFile> repoFileContent;
		List<ReposInfo> reposListStars;
		List<string> urlList = new List<string>();
		string urlListPath;

		public async Task GetRepos() {
			showList = false;
			var apiUrl = $"api/repos/{owner}";
			reposListStars = await HttpClient.GetJsonAsync<List<ReposInfo>>(apiUrl);
		}

		public async Task<string> GetRepoFiles(string _repoName) {
			urlList.Clear();
			urlListPath = "";
			EditorClose();
			repoName = _repoName;
			showDone = false;
			reposListStars = null;
			var apiUrlFiles = $"api/files/{owner}/{repoName}";
			reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}";
			reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
			showList = true;
			return repoName;
		}

		public void ListClose() {
			showList = false;
			urlList.Clear();
			urlListPath = "";
		}

		public void EditorClose() {
			commitMessage = "";
			showEditor = false;
			showEditorNewFile = false;
		}

		public async Task<List<string>> GetRepoFilesPath(string _path) {
			EditorClose();
			if (IsFile) {
				PreviousFolder().GetAwaiter();
			}
			urlList.Add(_path);
			urlListPath = String.Join("/", urlList);
			reposListStars = null;
			var apiUrlFiles = $"api/files/{owner}/{repoName}/{urlListPath}";
			reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}/{urlListPath}";
			reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
			IsFile = false;
			return urlList;
		}

		public async Task PreviousFolder() {
			EditorClose();
			showDone = false;
			urlList.RemoveAt(urlList.Count - 1);
			urlListPath = String.Join("/", urlList);
			var apiUrlFolders = $"api/folders/{owner}/{repoName}/{urlListPath}";
			var apiUrlFiles = $"api/files/{owner}/{repoName}/{urlListPath}";
			reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
		}

		public async Task<List<string>> GetFile(string _path) {
			if (IsFile) {
				PreviousFolder().GetAwaiter();
			}
			EditorClose();
			urlList.Add(_path);
			urlListPath = String.Join("/", urlList);
			reposListStars = null;
			var apiUrl = $"api/file/{owner}/{repoName}/{urlListPath}";
			repoFileContent = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrl);
			showEditor = true;
			IsFile = true;
			return urlList;
		}

		public void UpdateFile() {
			var apiUrl = $"api/update/{owner}/{repoName}/{commitMessage}/{urlListPath}";
			HttpClient.PostJsonAsync<string>(apiUrl, content);
			EditorClose();
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		public async Task DeleteFile() {
			var apiUrl = $"api/delete/{owner}/{repoName}/{commitMessage}/{urlListPath}";
			await HttpClient.DeleteAsync(apiUrl);
			EditorClose();
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		public void ShowEditorNewFile() {
			showDone = false;
			showEditorNewFile = true;
		}

		public void CreateFile() {
			urlList.Add(newFileName + newFileExtension);
			urlListPath = String.Join("/", urlList);
			var apiUrl = $"api/create/{owner}/{repoName}/{commitMessage}/{urlListPath}";
			HttpClient.PostJsonAsync<string>(apiUrl, newContent);
			showEditorNewFile = false;
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		private void HandleValidSubmit() {
			Console.WriteLine("OnValidSubmit");
		}
	}
}