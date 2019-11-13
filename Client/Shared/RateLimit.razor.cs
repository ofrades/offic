using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Shared {

	/// <summary>
	/// RateLimit
	/// </summary>
	public partial class RateLimit {

		/// <summary>
		/// RateLimits
		/// </summary>
		/// <value></value>
		public string RateLimits;

		/// <summary>
		/// Inject HttpClient
		/// </summary>
		/// <value></value>
		[Inject]
		public HttpClient HttpClient { get; set; }

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