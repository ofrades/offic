using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// Interface Markdown Service
	/// </summary>
	public interface IMdService {

		/// <summary>
		/// Get Most Starred Repos
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		Task<IEnumerable<GitRepo>> GetMostStarredRepos (string owner);
	}
}