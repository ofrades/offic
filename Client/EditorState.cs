using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared.Models;
using Shared.Validation;

namespace Client {

    public class EditorState {
        [Inject]
        AppState AppState { get; }

        [Inject]
        ListState ListState { get; }
        private readonly HttpClient HttpClient;
        public EditorState(HttpClient _HttpClient, AppState appState, ListState listState) {
            this.HttpClient = _HttpClient;
            AppState = appState;
            ListState = listState;
        }
        public event Action OnChange;
        public bool showDone { get; private set; } = false;
        public UpdateContent UpdateContent { get; set; } = new UpdateContent();
        public NewContent NewContent { get; set; } = new NewContent();

        public void UpdateFile(string commitMessage, string content) {
            var apiUrl = $"api/update/{AppState.owner}/{AppState.repoName}/{commitMessage}/{ListState.urlListPath}";
            HttpClient.PostJsonAsync<string>(apiUrl, content);
            ListState.PreviousFolder().GetAwaiter();
            ListState.EditorClose();
            ShowDone();
            NotifyStateChanged();
        }

        public async Task DeleteFile(string commitMessage) {
            var apiUrl = $"api/delete/{AppState.owner}/{AppState.repoName}/{commitMessage}/{ListState.urlListPath}";
            await HttpClient.DeleteAsync(apiUrl);
            ListState.PreviousFolder().GetAwaiter();
            ListState.EditorClose();
            ShowDone();
            NotifyStateChanged();
        }

        public void CreateFile(string commitMessage, string newContent, string newFileName, string newFileExtension) {
            var x = ListState.urlList;
            x.Add(newFileName + newFileExtension);
            ListState.urlListPath = String.Join("/", x);
            var apiUrl = $"api/create/{AppState.owner}/{AppState.repoName}/{commitMessage}/{ListState.urlListPath}";
            HttpClient.PostJsonAsync<string>(apiUrl, newContent);
            ListState.EditorClose();
            ListState.PreviousFolder().GetAwaiter();
            ShowDone();
            NotifyStateChanged();
        }

        public void ShowDone() {
            showDone = true;
            Task.Delay(2000);
            showDone = false;
        }
        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}