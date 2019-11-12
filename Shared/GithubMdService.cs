using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// GithubMdService
	/// </summary>
	public class GithubMdService: IMdService {

		private GitRepository _githubRepo;

		/// <summary>
		/// GithubMdService Constructor
		/// </summary>
		/// <param name="githubRepo"></param>
		public GithubMdService (GitRepository githubRepo) {
			_githubRepo = githubRepo;
		}

		/// <summary>
		/// GetMostStarredRepos
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public async Task<IEnumerable<GitRepo>> GetMostStarredRepos (string user) {
			var gitRepos = await _githubRepo.SearchAsync (user, DateTimeOffset.Now.AddYears (-5), DateTimeOffset.Now.AddDays (-7), 1, 10, "stars", "desc");

			return gitRepos.Items.Select (r => new GitRepo (r.Full_Name, r.Description, r.Stargazers_Count));
		}
	}
}