using System.Collections.Generic;

namespace Shared {

	/// <summary>
	/// Search Files Class
	/// </summary>
	public class GitSearch {

		/// <summary>
		/// search
		/// </summary>
		public string search;

		/// <summary>
		/// Total Count
		/// </summary>
		/// <value></value>
		public int TotalCount { get; protected set; }

		/// <summary>
		/// Incomplete Results
		/// </summary>
		/// <value></value>
		public bool IncompleteResults { get; protected set; }

		/// <summary>
		/// Items
		/// </summary>
		/// <value></value>
		public IReadOnlyList<string> Items { get; protected set; }
		
	}
}