using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared {

	/// <summary>
	/// Repo List Files
	/// </summary>
	public class RepoFileContent {

		/// <summary>
		/// Parameterless Constructor needed for deserialization
		/// </summary>
		public RepoFileContent(){}

		/// <summary>
		/// GitRepo Constructor
		/// </summary>
		/// <param name="content"></param>
		/// <param name="name"></param>
		public RepoFileContent(string content, string name) {
			Content = content;
			Name = name;
		}

		/// <summary>
		/// Repo File Content
		/// </summary>
		/// <value></value>
		public string Content { get; set; }

		/// <summary>
		/// Repo File Name
		/// </summary>
		/// <value></value>
		public string Name { get; set; }
	}
}