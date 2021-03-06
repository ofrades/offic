namespace Shared.Models {

	/// <summary>
	/// Owner Repos info 
	/// </summary>
	public class ReposInfo {

		/// <summary>
		/// Parameterless Constructor needed for deserialization
		/// </summary>
		public ReposInfo() { }

		/// <summary>
		/// Repo Constructor
		/// </summary>
		/// <param name="name"></param>
		/// <param name="fullName"></param>
		/// <param name="description"></param>
		/// <param name="stargazersCount"></param>
		public ReposInfo(string name, string fullName, string description, int stargazersCount) {
			Name = name;
			FullName = fullName;
			Description = description;
			StargazersCount = stargazersCount;
		}

		/// <summary>
		/// Repo Constructor Sort by Stars
		/// </summary>
		/// <param name="stargazersCount"></param>
		public ReposInfo(int stargazersCount) {
			StargazersCount = stargazersCount;
		}

		/// <summary>
		/// Repo Name
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