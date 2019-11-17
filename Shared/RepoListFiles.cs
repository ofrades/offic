using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// Repo List Files
	/// </summary>
	public class RepoListFiles {

		/// <summary>
		/// Parameterless Constructor needed for deserialization
		/// </summary>
		public RepoListFiles(){}

		/// <summary>
		/// GitRepo Constructor
		/// </summary>
		/// <param name="name"></param>
		public RepoListFiles(string name) {
			Name = name;
		}

		/// <summary>
		/// Repo File Name
		/// </summary>
		/// <value></value>
		public string Name { get; set; }
	}
}