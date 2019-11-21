using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Client.Validation;

namespace Client.Components {

	public partial class ListFiles {
		[Inject]
		HttpClient HttpClient { get; set; }
		bool showEditor = false;
		bool showEditorNewFile = false;
		bool showDone = false;
		bool showList = false;
		bool IsFile = false;
		Repo Repo { get; set; } = new Repo();
		UpdateContent UpdateContent { get; set; } = new UpdateContent();
		NewContent NewContent { get; set; } = new NewContent();
		List<RepoFile> _reposList;
		List<RepoFile> _reposFolders;
		List<RepoFile> _repoFileContent;
		List<ReposInfo> _reposListStars;
		List<string> _urlList = new List<string>();
		string _urlListPath;

		async Task GetRepos() {
			showList = false;
			var apiUrl = $"api/repos/{Repo.owner}";
			_reposListStars = await HttpClient.GetJsonAsync<List<ReposInfo>>(apiUrl);
		}

		async Task<string> GetRepoFiles(string _repoName) {
			_urlList.Clear();
			_urlListPath = "";
			EditorClose();
			Repo.repoName = _repoName;
			showDone = false;
			_reposListStars = null;
			var apiUrlFiles = $"api/files/{Repo.owner}/{Repo.repoName}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{Repo.owner}/{Repo.repoName}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
			showList = true;
			return Repo.repoName;
		}

		void ListClose() {
			showList = false;
			_urlList.Clear();
			_urlListPath = "";
		}

		void EditorClose() {
			UpdateContent.commitMessage = "";
			NewContent.commitMessage = "";
			showEditor = false;
			showEditorNewFile = false;
		}

		async Task<List<string>> GetRepoFilesPath(string _path) {
			EditorClose();
			if (IsFile) {
				PreviousFolder().GetAwaiter();
			}
			_urlList.Add(_path);
			_urlListPath = String.Join("/", _urlList);
			_reposListStars = null;
			var apiUrlFiles = $"api/files/{Repo.owner}/{Repo.repoName}/{_urlListPath}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			var apiUrlFolders = $"api/folders/{Repo.owner}/{Repo.repoName}/{_urlListPath}";
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
			IsFile = false;
			return _urlList;
		}

		async Task PreviousFolder(){
			EditorClose();
			showDone = false;
			_urlList.RemoveAt(_urlList.Count - 1);
			_urlListPath = String.Join("/", _urlList);
			var apiUrlFolders = $"api/folders/{Repo.owner}/{Repo.repoName}/{_urlListPath}";
			var apiUrlFiles = $"api/files/{Repo.owner}/{Repo.repoName}/{_urlListPath}";
			_reposList = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFiles);
			_reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrlFolders);
		}

		async Task<List<string>> GetFile(string _path) {
			if (IsFile) {
				PreviousFolder().GetAwaiter();
			}
			EditorClose();
			_urlList.Add(_path);
			_urlListPath = String.Join("/", _urlList);
			_reposListStars = null;
			var apiUrl = $"api/file/{Repo.owner}/{Repo.repoName}/{_urlListPath}";
			_repoFileContent = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrl);
			showEditor = true;
			IsFile = true;
			return _urlList;
		}

		void UpdateFile() {
			var apiUrl = $"api/update/{Repo.owner}/{Repo.repoName}/{UpdateContent.commitMessage}/{_urlListPath}";
			HttpClient.PostJsonAsync<string>(apiUrl, UpdateContent.content);
			EditorClose();
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		async Task DeleteFile() {
			var apiUrl = $"api/delete/{Repo.owner}/{Repo.repoName}/{UpdateContent.commitMessage}/{_urlListPath}";
			await HttpClient.DeleteAsync(apiUrl);
			EditorClose();
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		void ShowEditorNewFile() {
			showDone = false;
			showEditorNewFile = true;
		}

		void CreateFile() {
			_urlList.Add(NewContent.newFileName + NewContent.newFileExtension);
			_urlListPath = String.Join("/", _urlList);
			var apiUrl = $"api/create/{Repo.owner}/{Repo.repoName}/{NewContent.commitMessage}/{_urlListPath}";
			HttpClient.PostJsonAsync<string>(apiUrl, NewContent.newContent);
			showEditorNewFile = false;
			PreviousFolder().GetAwaiter();
			showDone = true;
		}

		private void HandleValidSubmit() {
			Console.WriteLine("OnValidSubmit");
		}

		protected override async Task OnInitializedAsync(){
			_reposListStars = await HttpClient.GetJsonAsync<List<ReposInfo>>($"api/repos/");
		}
	}
}