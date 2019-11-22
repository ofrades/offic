using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Core;
using Octokit;

namespace Server {
    
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeClient {
        private readonly IHttpContextAccessor _httpContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public AuthorizeClient(
            IHttpContextAccessor httpContext
        ){
            _httpContext = httpContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GitHubClient> Authorize() {
			var authResult = await _httpContext.HttpContext.AuthenticateAsync();
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
