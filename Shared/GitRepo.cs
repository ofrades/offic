using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// Git Repo
	/// </summary>
	public class GitRepo {

		/// <summary>
		/// Git Repo Constructor
		/// </summary>
		/// <param name="fullName"></param>
		/// <param name="description"></param>
		/// <param name="stargazersCount"></param>
		public GitRepo (string fullName, string description, int stargazersCount) {
			FullName = fullName;
			Description = description;
			StargazersCount = stargazersCount;
		}

		/// <summary>
		/// Git Repo Full Name
		/// </summary>
		/// <value></value>
		public string FullName { get; set; }

		/// <summary>
		/// Git Repo Description
		/// </summary>
		/// <value></value>
		public string Description { get; set; }

		/// <summary>
		/// Git Repo Stars
		/// </summary>
		/// <value></value>
		public int StargazersCount { get; set; }
	}
}