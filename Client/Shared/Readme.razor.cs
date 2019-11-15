using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using Client.Shared;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using Markdig;
using System.Text;

namespace Client.Shared {

	/// <summary>
	/// Index
	/// </summary>
	public partial class Readme {

		/// <summary>
		/// Is Loading
		/// </summary>
		public bool IsLoading = false;

		[Inject]
		public NavigationManager navigationManager { get; set; }


		/// <summary>
		/// owner
		/// </summary>
		/// <value></value>
		[Parameter]
		public string owner { get; set; }

		/// <summary>
		/// repoName
		/// </summary>
		/// <value></value>
		[Parameter]
		public string repoName { get; set; }

		[Parameter]
		public string updatedContent { get; set; }

		/// <summary>
		/// Readme
		/// </summary>
		public GitReadme _readme;

		/// <summary>
		/// Inject HttpClient
		/// </summary>
		/// <value></value>
		[Inject]
		public HttpClient HttpClient { get; set; }

		/// <summary>
		/// GetReadme
		/// </summary>
		/// <returns></returns>
		public async Task GetReadme() {
			var apiUrl = $"api/{owner}/{repoName}/readme";
			_readme = await HttpClient.GetJsonAsync<GitReadme>(apiUrl);
		}

		/// <summary>
		/// reposList
		/// </summary>
		public List<GitRepo> _reposList;

		/// <summary>
		/// GetRepos
		/// </summary>
		/// <returns></returns>
		public async Task GetRepos() {
			var apiUrl = $"api/{owner}/repos";
			_reposList = await HttpClient.GetJsonAsync<List<GitRepo>>(apiUrl);
		}
		// UpdateForm updateForm = new UpdateForm();
		/// <summary>
		/// UpdateFile
		/// </summary>
		/// <returns></returns>
		public async Task UpdateFile() {
			var apiUrl = $"{navigationManager.BaseUri}api/{owner}/{repoName}/update";

			var stringContent = new StringContent(updatedContent, Encoding.UTF8, "application/json");
			var response = await HttpClient.PostAsync(apiUrl, stringContent);

			// var update = await HttpClient.GetJsonAsync<UpdateForm>(apiUrl);
		}
		
		/// <summary>
		/// SubmitValidForm
		/// </summary>
		public void SubmitValidForm() {
			Console.WriteLine("Form Submitted Successfully!");
		}
	}
}