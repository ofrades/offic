using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Octokit;

namespace Server {

	/// <summary>
	/// Creates New Client Class
	/// </summary>
	public class CreateClient : Controller{

		/// <summary>
		/// New Client Method
		/// </summary>
		/// <returns>Client</returns>
		public  async Task<GitHubClient> NewClient() {
			var authResult = await HttpContext.AuthenticateAsync();
			if (!authResult.Succeeded) {
				throw new Exception("Not Authenticated");
			}
			var accessToken = authResult.Properties.Items[".Token.access_token"];
			var client = new GitHubClient(new ProductHeaderValue("offic"));
			client.Credentials = new Credentials(accessToken);
			return client;
		}
	}
}