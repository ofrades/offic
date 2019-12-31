using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared.Models;

namespace Client {
    public class AppState {
        private readonly HttpClient HttpClient;
        public AppState(HttpClient _HttpClient) {
            HttpClient = _HttpClient;
        }
        public event Action OnChange;
        public string owner { get; private set; }
        public string repoName { get; private set; }
        public bool showList { get; private set; } = false;
        public bool emptyList { get; private set; } = false;
        public List<ReposInfo> reposListStars { get; private set; }
        public List<RepoFile> repoList { get; private set; }
        public List<RepoFile> reposFolders { get; private set; }

        public async Task GetRepos(string _owner) {
            owner = _owner;
            reposListStars = await HttpClient.GetJsonAsync<List<ReposInfo>>($"api/repos/{owner}");
            NotifyStateChanged();
        }

        public async Task GetRepoFiles(string _repoName, string _path = "") {
            try {
                repoName = _repoName;
                repoList = await HttpClient.GetJsonAsync<List<RepoFile>>($"api/files/{owner}/{repoName}/{_path}");
                reposFolders = await HttpClient.GetJsonAsync<List<RepoFile>>($"api/folders/{owner}/{repoName}/{_path}");
                showList = true;
            } catch (System.Exception) {
                emptyList = true;
            }
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}