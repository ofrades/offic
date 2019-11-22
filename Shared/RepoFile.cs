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
		/// Repo Constructor
		/// </summary>
		/// <param name="content"></param>
		/// <param name="name"></param>
		public RepoFile(string content, string name) {
			Content = content;
			Name = name;
		}

		/// <summary>
		/// Repo Constructor
		/// </summary>
		/// <param name="name"></param>
		public RepoFile(string name) {
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