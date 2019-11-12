using System.Threading.Tasks;
using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Server {

    /// <summary>
    /// User Controller
    /// </summary>
	[ApiController]
	public class UserController : Controller {
		private static UserInfo LoggedOutUser = new UserInfo { IsAuthenticated = false };


        /// <summary>
        /// Get User
        /// </summary>
        /// <returns></returns>
		[HttpGet ("user")]
		public UserInfo GetUser () {
			return User.Identity.IsAuthenticated ?
				new UserInfo { Name = User.Identity.Name, IsAuthenticated = true } :
				LoggedOutUser;
		}

        /// <summary>
        /// Sign In
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
		[HttpGet ("user/signin")]
		public async Task SignIn (string redirectUri) {
			if (string.IsNullOrEmpty (redirectUri) || !Url.IsLocalUrl (redirectUri)) {
				redirectUri = "/";
			}

			await HttpContext.ChallengeAsync (
				GitHubAuthenticationDefaults.AuthenticationScheme,
				new AuthenticationProperties { RedirectUri = redirectUri });
		}

        /// <summary>
        /// Sign Out
        /// </summary>
        /// <returns></returns>
		[HttpGet ("user/signout")]
		public async Task<IActionResult> SignOut () {
			await HttpContext.SignOutAsync (CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect ("~/");
		}
	}
}