using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;
using Client.Shared;

namespace Client.Shared {

	/// <summary>
	/// Index
	/// </summary>
	public partial class Readme {

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

		RateLimit newRateLimit = new RateLimit();

		/// <summary>
		/// GetReadme
		/// </summary>
		/// <returns></returns>
		public async Task GetReadme() {
			var apiUrl = $"api/{owner}/{repoName}/readme";
			_readme = await HttpClient.GetJsonAsync<GitReadme>(apiUrl);
			await newRateLimit.GetApiLimit();
		}

		/// <summary>
		/// GetReadme
		/// </summary>
		/// <returns></returns>
		public async Task UpdateFile() {
			var apiUrl = $"api/{owner}/{repoName}/update";
			_update = await HttpClient.PostJsonAsync<string>(apiUrl, updateForm.updatedContent);
			await newRateLimit.GetApiLimit();
		}
		
		/// <summary>
		/// SubmitValidForm
		/// </summary>
		public void SubmitValidForm() {
			Console.WriteLine("Form Submitted Successfully!");
		}
	}
}