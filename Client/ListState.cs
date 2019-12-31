using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared.Models;
namespace Client {
    public class ListState {
        [Inject]
        AppState AppState { get; }
        private readonly HttpClient HttpClient;
        public ListState(HttpClient _HttpClient, AppState appState) {
            HttpClient = _HttpClient;
            AppState = appState;
        }
        public event Action OnChange;
        public bool showList { get; private set; } = false;
        public bool showEditor { get; private set; } = false;
        public bool showEditorNewFile { get; private set; } = false;
        public bool emptyList { get; private set; } = false;
        public List<string> urlList = new List<string>();
        public string urlListPath;
        public List<RepoFile> repoFileContent { get; private set; }
        public bool IsFile { get; set; }

        public async Task<List<string>> GetFile(string _path) {
            if (IsFile) {
                PreviousFolder().GetAwaiter();
            }
            EditorClose();
            urlList.Add(_path);
            urlListPath = String.Join("/", urlList);
            var apiUrl = $"api/file/{AppState.owner}/{AppState.repoName}/{urlListPath}";
            repoFileContent = await HttpClient.GetJsonAsync<List<RepoFile>>(apiUrl);
            showEditor = true;
            IsFile = true;
            NotifyStateChanged();
            return urlList;
        }

        public async Task<List<string>> GetRepoFilesPath(string _path) {
            EditorClose();
            if (IsFile) {
                PreviousFolder().GetAwaiter();
            }
            urlList.Add(_path);
            urlListPath = String.Join("/", urlList);
            await AppState.GetRepoFiles(AppState.repoName, _path);
            IsFile = false;
            NotifyStateChanged();
            return urlList;
        }

        public async Task PreviousFolder() {
            EditorClose();
            urlList.RemoveAt(urlList.Count - 1);
            urlListPath = String.Join("/", urlList);
            await AppState.GetRepoFiles(AppState.repoName, urlListPath);
            NotifyStateChanged();
        }

        public void ListClose() {
            showList = false;
            urlList.Clear();
            urlListPath = "";
            NotifyStateChanged();
        }

        public void ShowEditorNewFile() {
            showEditorNewFile = true;
            NotifyStateChanged();
        }

        public void EditorClose() {
            showEditor = false;
            showEditorNewFile = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}