using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;

namespace Client.Components
{

	/// <summary>
	/// Index
	/// </summary>
	public partial class Readme {

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

		/// <summary>
		/// Content Update
		/// </summary>
		/// <value></value>
		[Parameter]
		public string content { get; set; }

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

		/// <summary>
		/// UpdateFile
		/// </summary>
		/// <returns></returns>
		public async Task UpdateFile() {
			var apiUrl = $"api/{owner}/{repoName}/update";
			// var markdown = Markdown.Normalize(updatedContent);
			var update = await HttpClient.PostJsonAsync<string>(apiUrl, content);
		}
		
		/// <summary>
		/// SubmitValidForm
		/// </summary>
		public void SubmitValidForm() {
			Console.WriteLine("Form Submitted Successfully!");
		}
	}
}