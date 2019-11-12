using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Markdig;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Octokit;
using Shared;

namespace Client.Pages.CodeBehind
{

	/// <summary>
	/// Index
	/// </summary>
	public partial class Index : ComponentBase {

		/// <summary>
		/// Is Loading
		/// </summary>
		public bool IsLoading = false;

		/// <summary>
		/// updateForm
		/// </summary>
		/// <returns></returns>
		public UpdateForm updateForm {get;set;} = new UpdateForm();

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
		/// RateLimits
		/// </summary>
		/// <value></value>
		public string RateLimits;

		/// <summary>
		/// Readme
		/// </summary>
		public GitReadme _readme;

		/// <summary>
		/// Readme
		/// </summary>
		public string _update;

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
			await GetApiLimit();
		}

		/// <summary>
		/// GetReadme
		/// </summary>
		/// <returns></returns>
		public async Task UpdateFile() {
			var apiUrl = $"api/{owner}/{repoName}/update";
			_update = await HttpClient.PostJsonAsync<string>(apiUrl, updateForm.updatedContent);
			await GetApiLimit();
		}
		
		/// <summary>
		/// SubmitValidForm
		/// </summary>
		public void SubmitValidForm() {
			Console.WriteLine("Form Submitted Successfully!");
		}

		/// <summary>
		/// GetApiLimits
		/// </summary>
		/// <returns></returns>
		public async Task GetApiLimit() {
			var apiUrl = $"api/limits";
			RateLimits = await HttpClient.GetStringAsync(apiUrl);
		}

		/// <summary>
		/// OnInitializedAsync
		/// </summary>
		/// <returns></returns>
		protected override async Task OnInitializedAsync(){
			await GetApiLimit();
		}
	}
}