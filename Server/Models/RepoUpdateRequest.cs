namespace Server.Models {

	/// <summary>
	/// RepoUpdateRequest
	/// </summary>
	public class RepoUpdateRequest {

		/// <summary>
		/// Readme
		/// </summary>
		/// <value></value>
		public string Readme {get; set;}

		/// <summary>
		/// User
		/// </summary>
		/// <value></value>
		public string User {get;set;} // ofrades/XX

		/// <summary>
		/// RepoName
		/// </summary>
		/// <value></value>
		public string RepoName {get;set;}

		/// <summary>
		/// Content
		/// </summary>
		/// <value></value>
		public string Content {get;set;}
	}
}
