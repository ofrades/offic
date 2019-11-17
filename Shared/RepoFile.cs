using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// Repo List Files
	/// </summary>
	public class RepoFile {

		/// <summary>
		/// Parameterless Constructor needed for deserialization
		/// </summary>
		public RepoFile(){}

		/// <summary>
		/// GitRepo Constructor
		/// </summary>
		/// <param name="content"></param>
		public RepoFile(string content) {
			Content = content;
		}

		/// <summary>
		/// Repo File Name
		/// </summary>
		/// <value></value>
		public string Content { get; set; }
	}
}