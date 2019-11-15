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
		/// Parameterless Constructor needed for deserialization
		/// </summary>
		public GitRepo(){}

		/// <summary>
		/// GitRepo Constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="fullName"></param>
		/// <param name="description"></param>
		/// <param name="stargazersCount"></param>
		public GitRepo(string name, string fullName, string description, int stargazersCount){
			Name = name;
			FullName = fullName;
			Description = description;
			StargazersCount = stargazersCount;
		}

		/// <summary>
		/// GitRepo Constructor Sort by Stars
		/// </summary>
		/// <param name="stargazersCount"></param>
		public GitRepo(int stargazersCount){
			StargazersCount = stargazersCount;
		}

		/// <summary>
		/// Git Repo Name
		/// </summary>
		/// <value></value>
		public string Name { get; set; }
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
		/// Git Repo StargazersCount
		/// </summary>
		/// <value></value>
		public int StargazersCount { get; set; }
	}
}