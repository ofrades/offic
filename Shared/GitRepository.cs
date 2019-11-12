using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Shared {

	/// <summary>
	/// Repository
	/// </summary>
	public class GitRepository {
		private string GITHUB_CONTENT_URL => $"{UriParts.BASE_URL}/repos/{UriParts.OWNER}/{UriParts.REPO}/contents/{UriParts.PATH}";
		private string GITHUB_REPOS_URL => $"{UriParts.BASE_URL}/users/{UriParts.OWNER}/repos";

		private string GITHUB_REPOS_SEARCH_URL => $"{UriParts.BASE_URL}/search/repositories";

		private HttpClient HttpClient { get; set; }

		/// <summary>
		/// GitRepository Constructor
		/// </summary>
		/// <param name="httpClient"></param>
		public GitRepository (HttpClient httpClient) {
			HttpClient = httpClient;
		}


		/// <summary>
		/// SearchAsync
		/// </summary>
		/// <param name="owner"></param>
		/// <param name="createdDate"></param>
		/// <param name="lastUpdatedDate"></param>
		/// <param name="pageIndex"></param>
		/// <param name="itemsPerPage"></param>
		/// <param name="sortBy"></param>
		/// <param name="orderBy"></param>
		/// <returns></returns>
		public async Task<SearchResponse> SearchAsync (string owner, DateTimeOffset createdDate, DateTimeOffset lastUpdatedDate, int pageIndex, int itemsPerPage = 10, string sortBy = "stars", string orderBy = "desc") {
			var query = new [] {
					(Param: "user", Value : owner),
					(Param: "created", Value: $"{createdDate.ToString("yyyy-MM-dd")}..{DateTimeOffset.Now.ToString("yyyy-MM-dd")}"),
					(Param: "pushed", Value: $"{lastUpdatedDate.ToString("yyyy-MM-dd")}..{DateTimeOffset.Now.ToString("yyyy-MM-dd")}")
				}
				.Select (p => $"{p.Param}:{p.Value}")
				.Aggregate ((a, b) => $"{a}&{b}");

			var queryString = new [] {
					(Param: "page", Value : pageIndex.ToString ()),
					(Param: "per_page", Value : itemsPerPage.ToString ()),
					(Param: "order", Value : orderBy),
					(Param: "q", Value : query),
					(Param: "sort", Value : sortBy)
				}
				.Select (p => $"{p.Param}={p.Value}")
				.Aggregate ((a, b) => $"{a}&{b}");

			var url = $"{GITHUB_REPOS_SEARCH_URL}?{queryString}";
			return await HttpClient.GetJsonAsync<SearchResponse> (url);
		}
	}
}