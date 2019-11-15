using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using System.Collections.Generic;

namespace Client.Shared
{

	/// <summary>
	/// Index
	/// </summary>
	public partial class Create {

		/// <summary>
		/// Is Loading
		/// </summary>
		public bool IsLoading = false;

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
		/// Create Content
		/// </summary>
		/// <value></value>
		[Parameter]
		public string content { get; set; }

		/// <summary>
		/// Title of New File
		/// </summary>
		/// <value></value>
		[Parameter]
		public string title { get; set; }

		/// <summary>
		/// Inject HttpClient
		/// </summary>
		/// <value></value>
		[Inject]
		public HttpClient HttpClient { get; set; }

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

		CreateContent createContent = new CreateContent(content, title);

		/// <summary>
		/// CreateFile
		/// </summary>
		/// <returns></returns>
		public async Task CreateFile() {
			var apiUrl = $"api/{owner}/{repoName}/{title}/create";
			// var markdown = Markdown.Normalize(updatedContent);
			var update = await HttpClient.PostJsonAsync<string>(apiUrl, content);
		}

		protected bool showEditor = false;

		/// <summary>
		/// Show Editor
		/// </summary>
		public void ShowEditor() {
			showEditor = true;
		}
		
		/// <summary>
		/// SubmitValidForm
		/// </summary>
		public void SubmitValidForm() {
			Console.WriteLine("Form Submitted Successfully!");
		}
	}
}