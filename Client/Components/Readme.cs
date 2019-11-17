using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;

namespace Client.Components {

	public partial class Readme {

		[Parameter]
		public string owner { get; set; }

		[Parameter]
		public string repoName { get; set; }
		
		public string path = "README.md";

		[Parameter]
		public string content { get; set; }

		public RepoReadme _readme;

		[Inject]
		public HttpClient HttpClient { get; set; }

		public async Task GetReadme(string repoName) {
			// IsLoading = true;
			var apiUrl = $"api/{owner}/{repoName}/readme";
			_readme = await HttpClient.GetJsonAsync<RepoReadme>(apiUrl);
		}

		public List<Repo> _reposList;

		public async Task GetRepos() {
			// IsLoading = true;
			var apiUrl = $"api/{owner}/repos";
			_reposList = await HttpClient.GetJsonAsync<List<Repo>>(apiUrl);
		}

		public async Task UpdateFile() {
			var apiUrl = $"api/{owner}/{repoName}/update";
			try {
				// IsLoading = true;
				await HttpClient.PostJsonAsync<string>(apiUrl, content);	
			}
			catch (System.Exception) {
				
				throw;
			}
			Console.WriteLine("Success");
		}

		public void SubmitValidForm() {
			Console.WriteLine("Form Submitted Successfully!");
		}
	}
}